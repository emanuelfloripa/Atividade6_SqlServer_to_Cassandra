using Atividade6_Cassandra.Models;
using Cassandra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Controllers
{
    public class CassandraCtr
    {
        /// Documentação
        /// https://docs.datastax.com/en/developer/csharp-driver/3.2/
        /// 

        private readonly string _keyspace = "atividade6";
        private readonly object _nomeTabelaNF = "notafiscal";
        private Cluster _cluster;
        private ISession _session;
        private PreparedStatement _prepareStatement;
        private BoundStatement _statement;

        public CassandraCtr()
        {
            OpenCassandraDB();
            CriarSeNaoExistirDataBase();
        }

        /// <summary>
        /// Faz um teste de conexão na base Cassandra em 127.0.0.1
        /// </summary>
        /// <returns></returns>
        public bool TesteConexao()
        {
            try
            {
                //TODO parametrizar o local do Cassandra no web.config.
                //Create a cluster instance using 3 cassandra nodes.
                var cluster = Cluster.Builder()
                  .AddContactPoints("127.0.0.1", "localhost")
                  .Build();
                //Create connections to the nodes using a keyspace
                var session = cluster.Connect(_keyspace);
                //Execute a query on a connection synchronously
                var rs = session.Execute("SELECT * FROM teste");
                //Iterate through the RowSet
                foreach (var row in rs)
                {
                    var value = row.GetValue<string>("name");
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private void CreateDataBase()
        {
            Debug.WriteLine("* Criando as tabelas.");
            ExecuteSql(CassandraCreateTables.Script);
        }

        /// <summary>
        /// Prepara a execução com um SQL com parâmetros.
        /// Deve ser seguido por StatementBind
        /// </summary>
        /// <param name="sql"></param>
        public void StatementPrepare(string sql)
        {
            _prepareStatement = _session.Prepare(sql);
        }
        /// <summary>
        /// Realiza o bind do Statement.
        /// <para>Deve ser executado somente após StatementPrepare.</para>
        /// </summary>
        /// <param name="parametros">Parâmetros do SQL</param>
        public void StatementBind(Object[] parametros)
        {
            _statement = _prepareStatement.Bind(parametros);
        }
        /// <summary>
        /// Deve ser executado após StatementPrepare() e StatementBind().
        /// </summary>
        /// <returns></returns>
        public RowSet ExecutePreparedStatement()
        {
            return _session.Execute(_statement);
        }

        /// <summary>
        /// Executa o SQL e retorna o 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public RowSet ExecuteSql(string sql)
        {
            return _session.Execute(sql);
        }

        /// <summary>
        /// Realiza o load de uma nota nfNumber
        /// </summary>
        /// <param name="nfNumber"></param>
        /// <returns>Lista de itens da nota</returns>
        public List<NotaFiscalModel> LoadNotaFiscal(int nfNumber)
        {
            var result = new List<NotaFiscalModel>();
            var sql = $"select * from notafiscal where nf = {nfNumber};";
            var lista = ExecuteSql(sql);

            foreach (var row in lista)
            {
                var item = new NotaFiscalModel();
                item.NF = row.GetValue<int>("nf");
                item.NomeCliente = row.GetValue<string>("nomecliente");
                item.Endereco = row.GetValue<string>("endereco");
                item.Valor = row.GetValue<double>("valor");
                item.DescricaoServico = row.GetValue<string>("descricaoservico");
                item.Quantidade = row.GetValue<int>("quantidade");
                item.ValorUnitario = row.GetValue<double>("valorunitario");
                item.NomeRecurso = row.GetValue<string>("nomerecurso");
                item.FuncaoRecurso = row.GetValue<string>("funcaorecurso");
                item.Taxa = row.GetValue<double>("taxa");
                item.Desconto = row.GetValue<double>("desconto");
                item.SubTotal = row.GetValue<double>("subtotal");

                result.Add(item);
            }

            return result;
        }
        /// <summary>
        /// Salva um relatório PDF da nota número nfNumber
        /// </summary>
        /// <param name="nfNumber"></param>
        public void ExportaPdfNota(int nfNumber)
        {
            var notas = LoadNotaFiscal(nfNumber);
            var pdf = new GeraPdf(notas);
            pdf.SaveToFile($"{nfNumber}.pdf");
        }

        /// <summary>
        /// Realizar o download para o HttpContext do relatório PDF para nfNumber parametrizado
        /// </summary>
        /// <param name="context"></param>
        /// <param name="nfNumber"></param>
        public bool DownloadPdf(HttpContext context, int nfNumber, out string erroMsg)
        {
            try
            {
                var notas = LoadNotaFiscal(nfNumber);
                var pdf = new GeraPdf(notas);
                var bytes = pdf.GetPdfBytes();
                WriteFileToResponse(context, bytes, $"{nfNumber}.pdf");
                erroMsg = "";
                return true;
            }
            catch (Exception e)
            {
                erroMsg = e.Message;
                return false;
            }

        }

        /// <summary>
        /// Escreve o HttpContext com os bytes 
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bytes"></param>
        /// <param name="fileName"></param>
        /// <see cref="http://blog.softartisans.com/2016/04/15/save-pdf-file-to-httpresponse/"/>
        private static void WriteFileToResponse(HttpContext context, byte[] bytes, string fileName)
        {
            var bytesLength = bytes.Length.ToString(CultureInfo.InvariantCulture);
            var response = context.Response;
            response.Clear();
            response.Buffer = true;
            response.AddHeader("Content-Length", bytesLength);
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.ContentType = MimeMapping.GetMimeMapping(fileName);
            response.BinaryWrite(bytes);
            response.Flush();
            response.End();
        }

        /// <summary>
        /// Verifica se a base existe no server Cassandra conectado. Senão ele cria.
        /// </summary>
        private void CriarSeNaoExistirDataBase()
        {
            var sql = $"SELECT table_name FROM system_schema.tables WHERE keyspace_name = '{_keyspace}' and table_name = '{_nomeTabelaNF}'; ";
            var result = ExecuteSql(sql);
            if (result.GetRows().Count() == 0)
            {
                CreateDataBase();
            }
        }

        /// <summary>
        /// Abre o banco Cassandra
        /// </summary>
        private void OpenCassandraDB()
        {
            _cluster = Cluster.Builder()
              .AddContactPoints("127.0.0.1", "localhost")
              .Build();
            _session = _cluster.Connect(_keyspace);
        }
    }
}
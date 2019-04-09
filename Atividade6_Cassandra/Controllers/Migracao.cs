using Atividade6_Cassandra.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Controllers
{
    /// <summary>
    /// Realiza a migração dos dados do SQL Server para a base Cassandra
    /// </summary>
    public class Migracao
    {
        public Migracao()
        {
            
        }


        // How to embedded a "Text file" inside of a C# project
        //   and read it as a resource from c# code:
        //
        // (1) Add Text File to Project.  example: 'myfile.txt'
        //
        // (2) Change Text File Properties:
        //      Build-action: EmbeddedResource
        //      Logical-name: myfile.txt      
        //          (note only 1 dot permitted in filename)
        //
        // (3) from c# get the string for the entire embedded file as follows:
        //
        //     string myfile = GetEmbeddedResourceFile("myfile.txt");
        /// <summary>
        /// https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetEmbeddedResourceFile(string filename)
        {
            //TODO tentando ler o script do arquivo
            var a = System.Reflection.Assembly.GetExecutingAssembly();
            using (var s = a.GetManifestResourceStream(filename))
            using (var r = new System.IO.StreamReader(s))
            {
                string result = r.ReadToEnd();
                return result;
            }
        }

        /// <summary>
        /// Executa a migração completa.
        /// <para>- Apaga a tabela NotaFiscal da base Cassandra</para>
        /// <para>- Obtem os dados da base SQL Server</para>
        /// <para>- Reimporta todos os dados novamente</para>
        /// </summary>
        public void ExecutaMigracao()
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            var con = new SqlConnection(connStr);
            con.Open();

            //nf, nomecliente, endereco, valor, descricaoservico, quantidade, valorunitario, nomerecurso, funcaorecurso, taxa, desconto, subtotal
            //0   1              2          3         4             5               6           7             8            9      10        11
            var command = new SqlCommand(ConsultaNotaFiscalSQL.Script, con);
            SqlDataReader dataReader;
            try
            {
                dataReader = command.ExecuteReader();
                var nf = new NotaFiscalModel();

                var cas = new CassandraCtr();

                //Limpa a tabela de dados destino
                cas.ExecuteSql("truncate notafiscal;");

                var sqli = "insert into notafiscal (id, nf, nomecliente, endereco, valor, descricaoservico, quantidade, valorunitario, " +
                    " nomerecurso, funcaorecurso, taxa, desconto, subtotal) values(uuid(), ?,?,?,?,?,?,?,?,?,?,?,?); ";
                cas.StatementPrepare(sqli);

                while (dataReader.Read())
                {
                    nf.NF = (int)dataReader.GetValue(0);
                    nf.NomeCliente = (string)dataReader.GetValue(1);
                    nf.Endereco = (string)dataReader.GetValue(2);

                    nf.Valor = (double)dataReader.GetValue(3);
                    nf.DescricaoServico = (string)dataReader.GetValue(4);
                    nf.Quantidade = (int)dataReader.GetValue(5);
                    nf.ValorUnitario = (double)dataReader.GetValue(6);
                    nf.NomeRecurso = (string)dataReader.GetValue(7);
                    nf.FuncaoRecurso = (string)dataReader.GetValue(8);
                    nf.Taxa = (double)dataReader.GetValue(9);
                    nf.Desconto = (double)dataReader.GetValue(10);
                    nf.SubTotal = (double)dataReader.GetValue(11);

                    cas.StatementBind(new object[] { nf.NF, nf.NomeCliente, nf.Endereco, nf.Valor, nf.DescricaoServico, nf.Quantidade,
                        nf.ValorUnitario, nf.NomeRecurso, nf.FuncaoRecurso, nf.Taxa, nf.Desconto, nf.SubTotal});
                    cas.ExecutePreparedStatement();
                }
            }
            finally 
            {
                command.Dispose();
                con.Close();
            }
        }
    }
}
using Cassandra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public CassandraCtr()
        {
            OpenCassandraDB();
            CriarSeNaoExistirDataBase();
        }


        public bool TesteConexao()
        {
            try
            {
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
            //TODO script para criar a base aqui
            Debug.WriteLine("* Criando as tabelas.");
        }

        public RowSet ExecuteSql(string sql, Object[] p)
        {
            var ps = _session.Prepare(sql);
            //...bind different parameters every time you need to execute
            var statement = ps.Bind(p);
            //Execute the bound statement with the provided parameters
            return _session.Execute(statement);
        }

        public RowSet ExecuteSql(string sql)
        {
            return _session.Execute(sql);
        }

        private void CriarSeNaoExistirDataBase()
        {
            var sql = $"SELECT table_name FROM system_schema.tables WHERE keyspace_name = '{_keyspace}' and table_name = '{_nomeTabelaNF}'; ";
            var result = ExecuteSql(sql);
            if (result.GetRows().Count() == 0)
            {
                CreateDataBase();
            }
            //TODO verificar o resultado e chamar a criação de tabelas
        }

        private void OpenCassandraDB()
        {
            _cluster = Cluster.Builder()
              .AddContactPoints("127.0.0.1", "localhost")
              .Build();
            _session = _cluster.Connect(_keyspace);
        }
    }
}
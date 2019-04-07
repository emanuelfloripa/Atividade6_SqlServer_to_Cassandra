using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Controllers
{
    public class Migracao
    {
        private string sql = "SELECT * FROM resource_qualification_value";

        public Migracao()
        {

        }


        public void TestConexaoCassandra()
        {

        }


        public void TesteLoadSQL()
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            var con = new SqlConnection(connStr);
            con.Open();

            var command = new SqlCommand(sql, con);
            SqlDataReader dataReader;
            try
            {
                dataReader = command.ExecuteReader();
                string xxx;
                while(dataReader.Read())
                {
                    xxx = (string)dataReader.GetValue(0);
                    System.Console.WriteLine(xxx);
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
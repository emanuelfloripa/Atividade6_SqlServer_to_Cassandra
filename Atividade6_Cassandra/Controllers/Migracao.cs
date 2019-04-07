using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Controllers
{
    public class Migracao
    {
        private string sql;

        public Migracao()
        {

        }

        public void LoadSQL()
        {


            string connStr = "";
            var con = new SqlConnection(connStr);
            con.Open();

            var command = new SqlCommand(sql, con);
            var adapter = new SqlDataAdapter();

            try
            {
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();


            }
            finally 
            {
                command.Dispose();
                con.Close();
            }



        }

    
    }
}
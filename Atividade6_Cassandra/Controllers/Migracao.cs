using Atividade6_Cassandra.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Controllers
{
    public class Migracao
    {
        public Migracao()
        {
            
        }


        public void TestConexaoCassandra()
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


        public void TesteLoadSQL()
        {
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            var con = new SqlConnection(connStr);
            con.Open();

            //NF NomeCliente Endereco DescricaoServico    Quantidade ValorUnitario   NomeRecurso FuncaoRecurso   Taxa Desconto    SubTotal
            //0  1           2          3                 4             5               6           7             8     9           10 
            var command = new SqlCommand(ConsultaNotaFiscalSQL.Script, con);
            SqlDataReader dataReader;
            try
            {
                dataReader = command.ExecuteReader();


                while (dataReader.Read())
                {
                    //xxx = (string)dataReader.GetValue(0);
                    //System.Console.WriteLine(xxx);
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
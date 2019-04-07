using System;
using Atividade6_Cassandra.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atividade6_Cassandra.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
        public void LoadSQL()
        {
            var mm =new Migracao();
            mm.TesteLoadSQL();
        }
    }
}

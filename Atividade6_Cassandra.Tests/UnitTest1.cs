using System;
using System.Collections.Generic;
using Atividade6_Cassandra.Controllers;
using Atividade6_Cassandra.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Atividade6_Cassandra.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GeraPdfTest()
        {
            var notas = new List<NotaFiscalModel>()
            {
                new NotaFiscalModel {NF= 1234, NomeCliente= "Emanuel", Endereco= "rua alakjalajkaja", Quantidade= 99,
                    ValorUnitario = 1.2, NomeRecurso = "recurso", FuncaoRecurso = "funcão", Taxa = 1,
                    Desconto = .1, DescricaoServico = "Descricão servico", Valor = 100, SubTotal =99.2
                }
            };

            var g = new GeraPdf(notas);
            g.SaveToFile("teste.pdf");
        }


        [TestMethod]
        public void ExecutaMigracao()
        {

            var mm = new Migracao();
            mm.ExecutaMigracao();
        }

        [TestMethod]
        public void OpenCassandraDB()
        {
            var cas = new CassandraCtr();
            var result = cas.TesteConexao();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoadNF()
        {
            var nf = 1901419;
            var notas = new CassandraCtr().LoadNotaFiscal(nf);
            Assert.AreEqual(notas.Count, 15);
            Assert.AreEqual(notas[0].NomeCliente, "RIC TV");

            nf = 1263687;
            notas = new CassandraCtr().LoadNotaFiscal(nf);
            Assert.AreEqual(notas.Count, 5);
            Assert.AreEqual(notas[0].NomeCliente, "De Bortoli Wines");
        }

        [TestMethod]
        public void GeraPdfDaNota()
        {
            var nf = 1901419;
            var ca = new CassandraCtr();
            ca.ExportaPdfNota(nf);            
        }
    }
}

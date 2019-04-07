using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Models
{
    public class NotaFiscalModel
    {
        public int NF { get; set; }
        public string NomeCliente { get; set; }
        public string Endereco { get; set; }
        public string DescricaoServico { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public string NomeRecurso { get; set; }
        public string FuncaoRecurso { get; set; }
        public double Taxa { get; set; }
        public double Desconto { get; set; }
        public double SubTotal { get; set; }
    }
}
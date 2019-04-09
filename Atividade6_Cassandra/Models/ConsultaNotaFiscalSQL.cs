using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Models
{
    public class ConsultaNotaFiscalSQL
    {
        public const string Script =
            "SELECT " 
            + " i.number NF,"
            + " COALESCE(c.name, 'n/a') NomeCliente, "
            + " COALESCE(c.address,'n/a') Endereco,  "
            + " i.value Valor, "
            + " COALESCE(ss.service_description, 'n/a') DescricaoServico, "
            + " ii.quantity Quantidade,"
            + " ii.unit_value ValorUnitario,"
            + " COALESCE(r.name, 'n/a') NomeRecurso,"
            + " COALESCE(rq.qualificatin_name, 'n/a') FuncaoRecurso,"
            + " ii.tax_percent Taxa,"
            + " ii.discount_percent Desconto,"
            + " ii.subtotal  SubTotal"
            + " FROM invoice i "
            + " JOIN customer c on i.customer_id = c.id_customer"
            + " JOIN invoice_item ii on ii.invoice_id = i.number"
            + " JOIN \"service\" ss on ss.service_id = ii.service_id"
            + " LEFT JOIN resource r on r.id_resource = ii.resource_id"
            + " LEFT JOIN resource_qualification_assignement ra on ra.resource_id = r.id_resource"
            + " LEFT JOIN resource_qualification rq on rq.id_resource_qualification = ra.qualification_id";
    }
}
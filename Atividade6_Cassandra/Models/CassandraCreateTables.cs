using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atividade6_Cassandra.Models
{
    /// <summary>
    /// Script de criação da base Cassandra e da tabela notafiscal
    /// </summary>
    public class CassandraCreateTables
    {

        public static string Script =
            @"create keyspace IF NOT EXISTS atividade6 with replication = {'class':'SimpleStrategy', 'replication_factor':3};
            use atividade6;

            drop table notafiscal;

            create table if not exists notafiscal (	
	            id uuid ,
	            nf int ,
	            nomecliente text,
	            endereco text,
	            valor double,
	            descricaoservico text,
	            quantidade int,
	            valorunitario double,
	            nomerecurso text,
	            funcaorecurso text,
	            taxa double,
	            desconto double,
	            subtotal double,
	            primary key ((id), nf)
            );
            create index if not exists nf_index on notafiscal(nf);

            drop table if exists teste;

            create table if not exists teste (
	            id int primary key,
	            name text,
	            lastname text
            );

            insert into teste (id, name, lastname) values (1, 'emanuel', 'espindola');";

    }
}
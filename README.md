# Atividade6_SqlServer_to_Cassandra

O sistema migra os dados de uma base SQL Server para o Cassandra.
A página web fornece o download do PDF de uma nota obtida do Cassandra.


* Foi desenvolvido em .Net c# com SQL Server como banco de dados.
* Precisei converter o dump do mysql para SqlServer. Arquivo na pasta SQL do projeto.
* Não consegui uma lib que produzisse um relatório PDF completo. Então gerei um pdf tabulando manualmente.
* O sistema web retorna o download do PDF da nota fiscal passada como parâmetro.
* Existe um módulo de login mas não é necessário logar.
* A migração é realizada na tela About.

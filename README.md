# Build-Tickets
aplicação para gerenciamento de tickets.

# Desafios/problemas 
Um dos desafios que encontrei foi mais a questão do layout que foi desenvolvido em React, apesar de não está sendo avaliado, quis desenvolver o front com essa tecnologia pois esta super em alta no mercado, e como estou estudando achei que seria uma boa oportunidade em utiliza-la.

# Maneiras através das quais você pode melhorar a aplicação
Melhoria que poderia realizar na aplicação na parte de layout, seria uma melhor distribuição das páginas e campos para melhor experiência do usuário. Na parte de processamento caso o sistema venha ter muitos Ticket, seria interessante pensar em uma paginação  no grid de visualização assim dependendo da quantidade de ticket a consulta ficara mais rápida.Talvez para melhor experiência do usuário e performasse da aplicação, seria separar os Ticket por “Status” em grids diferentes, assim criaria alguns métodos de listagem independentes, tornando a listagem dos tickets mais rápidos e melhor visualização da página.

# Tecnologia utilizadas

FrontEnd: React,
Backend: Asp.Net Core 3.1,
Banco de dados: Relacional: SQL SERVER

# procedimento para executar a aplicação:

# Base
1-Acesse seu SQL server e crie uma base de dados com o nome BuildOne.
2-Acesse a pasta com o tìtulo "Base" que estará na mesma pasta onde você clonou seu projeto e execute o script no banco de dados. 

# Aplicação
2-Acesso o arquivo "appsettings.Development.json" que estará dentro de "appsettings.json" que se encontra dentro do projeto "Presentation" e altere a propriedade "DevConnection"  com os dados do seu banco de dados.

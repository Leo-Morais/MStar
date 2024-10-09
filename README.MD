# README.MD

## Para o Back-End:
- O script SQL está na pasta `SQL`.
- A conexão com o banco de dados SQL Server está no arquivo `appsettings.json`, na seção "ConnectionStrings". Troque os dados para o logon do seu usuário e o seu banco de dados.
- Coloque seu logon com as seguintes responsabilidades: `db_datareader, db_datawriter, db_ddladmin, db_owner` e associe ele ao banco `MStarDB`.
- A porta em que a API está rodando é: `localhost:7116`. Se precisar trocar a porta, ela pode ser encontrada em `Properties` na parte de HTTP.

## Para o Front-End:
- Utilize `npm install` para instalar todas as dependências do projeto.

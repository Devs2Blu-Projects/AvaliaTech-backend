<h1 align="center">Sistema de avalição - Back-End</h1>
<h2>Integrantes 👯‍♀️</h2>

- <a href="https://github.com/caiodsj"><b>Caio Diego</b></a>

- <a href="https://github.com/corecl4ud"><b>Claudia Cabral</b></a>

- <a href="https://github.com/helenaluz"><b>Helena Luz</b></a>

- <a href="https://github.com/Rafael-RD"><b>Rafael R Demarch</b></a>

- <a href="https://github.com/raphael-teodoro"><b>Raphael C. Teodoro</b></a>

- <a href="https://github.com/rbcaputo"><b>Rob Caputo</b></a>

<h2>Tecnologias 💻</h2>

[![My Skills](https://skillicons.dev/icons?i=cs,visualstudio,dotnet&theme=light)](https://skillicons.dev) `Entity Framework` `SQL Server` `JWT`

<h2>Como rodar 💡</h2>

1. Instalar o <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/7.0">.NETCore 7.0</a> 
2. Clonar a <a href="https://visualstudio.microsoft.com/pt-br/downloads/">branch main</a> em seu PC
3. Na pasta "hackweek-backend" rodar "dotnet run" no terminal
4. Boa sorte 🍀

<h2>Documentação 📜</h2>

<h3>UML</h3>
<img src="https://cdn.discordapp.com/attachments/1158473449434534011/1162178937728794695/ClassesUML.png?ex=653afe7a&is=6528897a&hm=c58bf2b7428b7d95698cfebc73416299122b25942acec39c3cb8144f6c4746a3&" alt="bootstrap"  height="500"/>
<small>Feito com LucidChart</small>

<h2>Endpoints 📍</h2>

<h3>Criterio</h3>

- `POST/api/Criterion` Cria um novo critério.

- `GET/api/Criterion` Retorna todos os critérios.

- `GET/api/Criterion/{Id}` Retorna todos os critérios por Id.

- `GET/api/Criterion/event` Retorna todos os critérios do evento atual.

- `GET/api/Criterion/event/{Id}` Retorna todos os critérios de um evento por Id (do evento).

- `PUT/api/Criterion/{Id}` Atualiza um critério.

- `DELETE/api/Criterion/{Id}` Deleta um critério.

<h3>Evento</h3>

- `GET/api/Event` Retorna todos os eventos.

- `GET/api/Event/{Id}` Retorna um evento por Id.

- `POST/api/Event` Cria um novo evento.

- `DELETE/api/Event/{Id}` Deleta um evento.

- `PUT/api/Event/{Id}` Atualiza um evento.

<h3>Global</h3>

- `GET/api/Global/currentevent` Retorna o evento atual

- `PUT/api/Global/currentevent/{eventId}` Atualiaza o evento atual

- `PUT/api/Global/currentevent/public` "Abre" o evento atual ao público

- `PUT/api/Global/currentevent/closed` "Fecha" o evento atual

<h3>Grupo</h3>

- `GET/api/Group` Retorna todos os grupos

- `GET/api/Group/{id}` Retorna o grupo por Id

- `GET/api/Group/user/{idUser}` Retorna o grupo por representante

- `GET/api/Group/ranking` Retorna o ranking

- `GET/api/Group/rate/{idUser}` Retorna os grupos para avaliar

- `GET/api/Group/groupsByDate` Retorna os grupos por dia

- `PUT/api/Group/{id}` Atualiaza o grupo

<h3>Login</h3>

- `POST/api/Login` Faz o login

<h3>Desafio</h3>

- `GET/api/Propostion` Retorna todos os desafios
  
- `GET/api/Propostion/{id}` Retorna o desafio por Id
    
- `POST/api/Propostion` Cria um desafio
  
- `DELETE/api/Propostion/{id}` Deleta um desafio

- `PUT/api/Propostion/{id}` Atualiza um desafio

<h3>Avaliação</h3>

- `GET/api/Rating` Retorna todas as avaliações
  
- `GET/api/Rating/evaluator/{id}` Retorna todas as avaliações de um avaliador (user)

- `GET/api/Rating/group/{id}` Retorna todas as avaliações de um grupo 

- `POST/api/Rating` Cria uma avaliação

- `DELETE/api/Rating/{id}` Deleta uma avaliação

<h3>Usuários</h3>

- `GET/api/User/{id}` Retorna um usuário por Id

- `GET/api/User/role/{role}` Retorna usuarios por seu Role

- `GET/api/User/{id}/redefine` Retornar uma nova senha gerada

- `POST/api/User` Cria um usuário

- `DELETE/api/User/{id}` Deleta um usuário

- `PUT/api/User/{id}` Atualiza um usuário




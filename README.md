

<div align="center">
  
[![Typing SVG](https://readme-typing-svg.demolab.com?font=Exo+2&size=25&pause=1000&color=FFCD00&center=true&vCenter=true&random=false&width=435&lines=Sistema+de+Avalia%C3%A7%C3%A3o+Hackweek++;3%C2%AA+edi%C3%A7%C3%A3o++%2BDevs2Blu+-+Back-end)](https://git.io/typing-svg)

</div>

<div align="center">
  <p> O objetivo deste sistema é fornecer uma plataforma para avaliar aspectos específicos de apresentações, permitindo uma abordagem personalizada e rastreável para cada avaliador.</p>
    
  <p>Este back-end é projetado para gerenciar eficientemente o cadastro de critérios de avaliação, fornecer autenticação segura para avaliadores e permitir a coleta e análise de dados de avaliação. Além disso, o back-end é responsável por fornecer informações     sobre o ranking das apresentações, oferecendo uma visão abrangente do desempenho das equipes.</p>
  
</div>

<hr>

<div style='display: inline_block' align='center'>
  
  <p align="center"><a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com?font=Exo+2&size=25&pause=1000&color=216EC8&center=true&vCenter=true&random=false&width=435&lines=Tecnologias+utilizadas%3A" alt="Typing SVG"/></a></p>
  
  <img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white">
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"> 
   <img src="https://img.shields.io/badge/entity-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"> 
  
 </div>

 <hr>
 
<div align='center'>
<p><a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com?font=Exo+2&size=25&pause=1000&color=FFCD00&center=true&vCenter=true&random=false&width=435&lines=Como+rodar+o+projeto%3A" alt="Typing SVG" /></a></p>
  
Aqui estão duas maneiras de executar o projeto do back-end da Hackweek, usando o Visual Studio e a linha de comando.

### Usando o `Visual Studio:`

`1.` **Clone o Repositório**: Você precisará obter o código-fonte do projeto. Você pode clonar o repositório do GitHub usando as opções do Visual Studio ou executando o seguinte comando em seu terminal:

``` git clone https://github.com/seu-usuario/hackweek-backend.git ```

Substitua `seu-usuario` pelo seu nome de usuário do GitHub ou use o URL correto do repositório. <br>


`2.` **Abra o Projeto**: Abra o Visual Studio e escolha "Arquivo" -> "Abrir" -> "Projeto/Solução" e selecione o arquivo `.csproj` no diretório do projeto. 


`3.` **Configure o Ambiente de Desenvolvimento**: Certifique-se de ter o .NET Core 7.0 instalado em seu sistema. <br>


`4.` **Execute o Projeto**: Você pode pressionar F5 ou clicar em "Iniciar" no Visual Studio para executar o projeto. 

`5.` **Acesse o Aplicativo**: O aplicativo estará acessível localmente. Você pode usar um navegador da web e acessar `http://localhost:7129` para interagir com o back-end do projeto Hackweek. 

<hr>



### Usando a `Linha de Comando`

`1.` **Navegue para a Pasta do Projeto**: Use o comando `cd` para entrar na pasta do projeto: cd `hackweek-backend`


`2.` **Rode o Projeto**: Você pode iniciar o projeto executando o seguinte comando: `dotnet run`. Isso iniciará o servidor e seu aplicativo estará acessível localmente.


`3.` **Acesse o Aplicativo**: Abra um navegador da web e acesse `http://localhost:7129` para interagir com o back-end do projeto Hackweek.

</div>


<hr>

<div align='center'>
<p><a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com?font=Exo+2&size=25&pause=1000&color=216EC8&center=true&vCenter=true&random=false&width=435&lines=Documenta%C3%A7%C3%A3o%3A" alt="Typing SVG" /></a></p>
<h4>UML (Feita com LucidChart)</h4> 
<img src="https://github.com/Devs2Blu-Projects/AvaliaTech-backend/assets/119363998/531d985b-0a28-4385-839b-6c05b876ddea"> 
</div>

<hr>

<br>

<div align='center'>
<p ><a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com?font=Exo+2&size=25&pause=1000&color=639CC8&center=true&vCenter=true&random=false&width=435&lines=Endpoints+%F0%9F%93%8D%3A" alt="Typing SVG" /></a></p>

### Critério

 `POST /Criterion`: Cria um novo critério.
 
 `GET /Criterion`: Retorna todos os critérios.
 
 `GET /Criterion/{Id}`: Retorna um critério por Id.
 
 `GET /Criterion/event`: Retorna todos os critérios do evento atual.
 
 `GET /Criterion/event/{Id}`: Retorna todos os critérios de um evento por Id (do evento).
 
 `PUT /Criterion/{Id}`: Atualiza um critério.
 
 `DELETE /Criterion/{Id}`: Deleta um critério.

<hr>

### Evento

 `GET /Event`: Retorna todos os eventos.
 
 `GET /Event/{Id}`: Retorna um evento por Id.
 
 `POST /Event`: Cria um novo evento.
 
 `DELETE /Event/{Id}`: Deleta um evento.
 
 `PUT /Event/{Id}`: Atualiza um evento.

<hr>

### Global

 `GET /Global/currentevent`: Retorna o evento atual.
 
 `PUT /Global/currentevent/{eventId}`: Atualiza o evento atual.
 
 `PUT /Global/currentevent/public`: Disponibiliza o resultado do evento ao público.
 
 `PUT /Global/currentevent/closed`: Encerra o período de avaliação do evento.

<hr>

### Grupo

 `GET /Group`: Retorna todos os grupos.
 
 `GET /Group/{id}`: Retorna um grupo por Id.
 
 `GET /Group/user/{idUser}`: Retorna o grupo por representante.
 
 `GET /Group/ranking`: Retorna o ranking.
 
 `GET /Group/rate/{idUser}`: Retorna os grupos para avaliar.
 
 `GET /Group/groupsByDate`: Retorna os grupos por dia.
 
 `PUT /Group/{id}`: Atualiza um grupo.

`PUT /Group/updateOrder` : Atualiza a ordem dos grupos
<hr>

### Login

 `POST /Login`: Faz o login.

<hr>

### Desafio

 `GET /Propostion`: Retorna todos os desafios.
 
 `GET /Propostion/{id}`: Retorna o desafio por Id.
 
 `POST /Propostion`: Cria um desafio.
 
 `DELETE /Propostion/{id}`: Deleta um desafio.
 
 `PUT /Propostion/{id}`: Atualiza um desafio.

<hr>

### Avaliação

 `GET /Rating`: Retorna todas as avaliações.
 
 `GET /Rating/evaluator/{id}`: Retorna todas as avaliações de um avaliador (usuário).
 
 `GET /Rating/group/{id}`: Retorna todas as avaliações de um grupo.
 
 `POST /Rating`: Cria uma avaliação.
 
 `DELETE /Rating/{id}`: Deleta uma avaliação.

<hr>

### Usuários

 `GET /User/{id}`: Retorna um usuário por Id.
 
 `GET User/role/{role}`: Retorna usuários por seu cargo (role).
 
 `GET /User/{id}/redefine`: Retorna uma nova senha gerada.
 
 `POST /User`: Cria um usuário.
 
 `DELETE /User/{id}`: Deleta um usuário.
 
 `PUT /User/{id}`: Atualiza um usuário.
</div>

<hr>

<div align='center'>
<p><a href="https://git.io/typing-svg"><img src="https://readme-typing-svg.demolab.com?font=Exo+2&size=25&pause=1000&color=FFCD00&&center=true&vCenter=true&random=false&width=435&lines=Integrantes%3A" alt="Typing SVG" /></a></p>
  <a align='center' href="https://github.com/caiodsj"><b>Caio Diego</b></a>
  <br>
  <a href="https://github.com/corecl4ud"><b>Claudia Cabral</b></a>
  <br>
  <a href="https://github.com/helenaluz"><b>Helena Luz</b></a>
  <br>
  <a href="https://github.com/Rafael-RD"><b>Rafael R Demarch</b></a>
  <br>
  <a href="https://github.com/raphael-teodoro"><b>Raphael C. Teodoro</b></a>
  <br>
  <a href="https://github.com/rbcaputo"><b>Rob Caputo</b></a>
</div>


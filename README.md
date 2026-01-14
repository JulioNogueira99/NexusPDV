# 🛒 NexusPDV

> Uma API robusta para gestão de Ponto de Venda (PDV), focada em integridade transacional, arquitetura desacoplada e segurança.

![Net](https://img.shields.io/badge/.NET%209-512BD4?style=flat&logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat&logo=docker&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-512BD4?style=flat&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=flat&logo=json-web-tokens&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-blue)

## 📖 Sobre o Projeto

O **NexusPDV** é um backend desenvolvido em .NET 9 para gerenciar vendas de um mini-mercado. 
O diferencial deste projeto não é apenas "fazer um CRUD", mas sim garantir a **consistência de dados** em operações complexas e a **segurança** de acesso.

Utiliza o padrão **Unit of Work** para assegurar que um Pedido só seja gerado se houver baixa de estoque bem-sucedida (transação atômica) e protege suas operações críticas através de **Autenticação JWT**.

## 🚀 Tecnologias & Práticas

* **Language:** C# (.NET 9)
* **Framework:** ASP.NET Core Web API
* **Container:** Docker & Docker Compose
* **Security:** JWT (JSON Web Tokens) & ASP.NET Core Identity
* **ORM:** Entity Framework Core (SQL Server)
* **Architecture:** Vertical Slice Architecture & Clean Architecture
* **Design Patterns:** CQRS, Mediator Pattern, Unit of Work, Domain-Driven Design (DDD).
* **Libraries:** MediatR(Orchestration), FluentValidation (Pipeline Behavior).
* **Testing:** xUnit + Moq (Unit Testing)
* **Documentation:** Swagger UI (Swashbuckle)

## 🏗️ Arquitetura

O projeto foi refatorado para utilizar **Vertical Slice Architecture** com **CQRS**, visando alta coesão e baixo acoplamento. As funcionalidades são isoladas em Features autocontidas, substituindo a camada tradicional de Services por Handlers orquestrados via MediatR.

NexusPDV
├── 📂 NexusPDV.Domain           # Entidades, Enums, Interfaces (Núcleo puro)
├── 📂 NexusPDV.Application      # A Camada de Use Cases (Coração da mudança)
│   ├── 📂 UseCases              # Organizado por Features (ex: Auth, Orders)
│   │   └── 📂 PlaceOrder        # Contém: Command, Handler, Validator e Response juntos
│   └── 📂 Behaviors             # Pipelines do MediatR (ex: ValidationBehavior)
├── 📂 NexusPDV.Infrastructure   # Banco de Dados (EF Core), Identity, Implementações
└── 📂 NexusPDV.API              # Controllers (Thin Controllers), Configurações, Dockerfile

### Destaques Técnicos

* **Secure by Design:** Rotas críticas (como criar pedidos) exigem autenticação via Token Bearer.
* **Rich Domain Models:** A lógica de "Baixar Estoque" reside dentro da entidade `Product`, protegendo o estado do objeto.
* **Transaction Management:** Uso de Unit of Work para garantir atomicidade entre tabelas.
* **Auto-Migration:** O sistema é capaz de criar o banco de dados e aplicar migrações automaticamente ao iniciar no container.

## 🐳 Como Rodar o Projeto (Docker)

A forma mais simples de rodar a aplicação (API + SQL Server) é utilizando o Docker. Você não precisa ter o .NET SDK ou SQL Server instalados na sua máquina.

### Pré-requisitos
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e rodando.

### Passo a Passo

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/JulioNogueira99/NexusPDV.git
    cd NexusPDV
    ```

2.  **Suba o ambiente:**
    Execute o comando abaixo na raiz do projeto. Ele irá compilar a API, baixar o SQL Server e configurar a rede.
    ```bash
    docker compose up --build
    ```

3.  **Acesse:**
    Abra o navegador em: [http://localhost:8080/swagger](http://localhost:8080/swagger)

> **Nota:** Na primeira execução, o SQL Server pode demorar alguns segundos para iniciar. Se a API falhar ao conectar, ela tentará reiniciar automaticamente até conseguir.

---

## 🔐 Como Acessar (Autenticação)

Como o sistema possui segurança JWT, o fluxo de uso no Swagger segue a ordem abaixo:

1.  **Crie seu Usuário:**
    Vá no endpoint `POST /api/Auth/register` e crie um login.
2.  **Faça Login:**
    Vá no endpoint `POST /api/Auth/login` com os dados criados.
    *Copie o `token` gerado na resposta.*
3.  **Autentique-se no Swagger:**
    Clique no botão **Authorize** 🔓 (cadeado) no topo da página.
    Digite: `Bearer SEU_TOKEN_AQUI` e clique em Login.
4.  **Use a API:**
    Agora você pode acessar as rotas protegidas (como criar vendas).

---

## 🔌 Endpoints Principais

### 🛡️ Auth (Autenticação)
* `POST /api/Auth/register` - Cria um novo usuário no sistema.
* `POST /api/Auth/login` - Retorna o Token JWT de acesso.

### 🛒 Orders (Vendas)
* `POST /api/Orders` - **[Requer Auth]** Realiza uma nova venda e baixa estoque.
    * *Body Exemplo:*
    ```json
    {
      "customerId": 1,
      "items": [
        { "productId": 1, "quantity": 1 }
      ]
    }
    ```
* `GET /api/Orders/{id}` - Consulta um pedido e seus itens.

## 🧪 Rodando os Testes (Opcional)

Se você tiver o .NET SDK instalado e quiser rodar os testes unitários da aplicação:

```bash
dotnet test
🤝 Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir Issues ou Pull Requests.

📝 Licença
Este projeto está sob a licença MIT.

Desenvolvido com 💜 por Júlio.
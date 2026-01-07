🛒 NexusPDV
Uma API robusta para gestão de Ponto de Venda (PDV), focada em integridade transacional, arquitetura desacoplada e segurança.

📖 Sobre o Projeto
O NexusPDV é um backend desenvolvido em .NET 9 para gerenciar vendas de um mini-mercado. O diferencial deste projeto não é apenas "fazer um CRUD", mas sim garantir a consistência de dados em operações complexas e a segurança de acesso.

Utiliza o padrão Unit of Work para assegurar que um Pedido só seja gerado se houver baixa de estoque bem-sucedida (transação atômica) e protege suas operações críticas através de Autenticação JWT.

🚀 Tecnologias & Práticas
Language: C# (.NET 9)

Framework: ASP.NET Core Web API

Container: Docker & Docker Compose

Security: JWT (JSON Web Tokens) & ASP.NET Core Identity

ORM: Entity Framework Core (SQL Server)

Architecture: Clean Architecture (Domain, Application, Infrastructure, API)

Design Patterns: Repository Pattern, Unit of Work, Domain-Driven Design (DDD).

Validation: FluentValidation

Testing: xUnit + Moq (Unit Testing)

Documentation: Swagger UI (Swashbuckle)

🏗️ Arquitetura
O projeto segue estritamente a Clean Architecture para garantir testabilidade e manutenção:

NexusPDV
├── 📂 NexusPDV.Domain          # Entidades, Enums, Interfaces (O Coração / Puro C#)
├── 📂 NexusPDV.Application     # Casos de Uso (Services), DTOs, Validações, Auth Logic
├── 📂 NexusPDV.Infrastructure  # Banco de Dados (EF Core), Identity, Repositórios
└── 📂 NexusPDV.API             # Controllers, Configurações JWT, Swagger, Dockerfile
Destaques Técnicos
Secure by Design: Rotas críticas (como criar pedidos) exigem autenticação via Token Bearer.

Rich Domain Models: A lógica de "Baixar Estoque" reside dentro da entidade Product, protegendo o estado do objeto.

Transaction Management: Uso de Unit of Work para garantir atomicidade entre tabelas.

Auto-Migration: O sistema é capaz de criar o banco de dados e aplicar migrações automaticamente ao iniciar no container.

🐳 Como Rodar o Projeto (Docker)
A forma mais simples de rodar a aplicação (API + SQL Server) é utilizando o Docker. Você não precisa ter o .NET SDK ou SQL Server instalados na sua máquina.

Pré-requisitos
Docker Desktop instalado e rodando.

Passo a Passo
Clone o repositório:

Bash

git clone https://github.com/JulioNogueira99/NexusPDV.git
cd NexusPDV
Suba o ambiente: Execute o comando abaixo na raiz do projeto. Ele irá compilar a API, baixar o SQL Server e configurar a rede.

Bash

docker compose up --build
Acesse: Abra o navegador em: http://localhost:8080/swagger

Nota: Na primeira execução, o SQL Server pode demorar alguns segundos para iniciar. Se a API falhar ao conectar, ela tentará reiniciar automaticamente até conseguir.

🔐 Como Acessar (Autenticação)
Como o sistema possui segurança JWT, o fluxo de uso no Swagger segue a ordem abaixo:

Crie seu Usuário: Vá no endpoint POST /api/Auth/register e crie um login.

Faça Login: Vá no endpoint POST /api/Auth/login com os dados criados. Copie o token gerado na resposta.

Autentique-se no Swagger: Clique no botão Authorize 🔓 (cadeado) no topo da página. Digite: Bearer SEU_TOKEN_AQUI e clique em Login.

Use a API: Agora você pode acessar as rotas protegidas (como criar vendas).

🔌 Endpoints Principais
🛡️ Auth (Autenticação)
POST /api/Auth/register - Cria um novo usuário no sistema.

POST /api/Auth/login - Retorna o Token JWT de acesso.

🛒 Orders (Vendas)
POST /api/Orders - [Requer Auth] Realiza uma nova venda e baixa estoque.

Body Exemplo:

JSON

{
  "customerId": 1,
  "items": [
    { "productId": 1, "quantity": 1 }
  ]
}
GET /api/Orders/{id} - Consulta um pedido e seus itens.

🧪 Rodando os Testes (Opcional)
Se você tiver o .NET SDK instalado e quiser rodar os testes unitários da aplicação:

Bash

dotnet test
🤝 Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir Issues ou Pull Requests.

📝 Licença
Este projeto está sob a licença MIT.

Desenvolvido com 💜 por Júlio.
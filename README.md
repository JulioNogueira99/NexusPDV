🛒 NexusPDV
Uma API robusta para gestão de Ponto de Venda (PDV), focada em integridade transacional e arquitetura desacoplada.

📖 Sobre o Projeto
O NexusPDV é um backend desenvolvido em .NET 9 para gerenciar vendas de um mini-mercado. 
O diferencial deste projeto não é apenas "fazer um CRUD", mas sim garantir a consistência de dados em operações complexas. 
Utiliza o padrão Unit of Work para assegurar que um Pedido só seja gerado se houver baixa de estoque bem-sucedida, tratando a venda como uma transação atômica.

🚀 Tecnologias & Práticas
Language: C# (.NET 9)

Framework: ASP.NET Core Web API

ORM: Entity Framework Core (SQL Server)

Architecture: Clean Architecture (Domain, Application, Infrastructure, API)

Design Patterns: Repository Pattern, Unit of Work, Domain-Driven Design (DDD), Dependency Injection.

Validation: FluentValidation

Testing: xUnit + Moq (Unit Testing)

Documentation: Swagger UI (Swashbuckle)

🏗️ Arquitetura
O projeto segue estritamente a Clean Architecture para garantir testabilidade e manutenção:

NexusPDV
├── 📂 NexusPDV.Domain          # Entidades, Enums, Interfaces (O Coração / Puro C#)
├── 📂 NexusPDV.Application     # Casos de Uso (Services), DTOs, Validações
├── 📂 NexusPDV.Infrastructure  # Banco de Dados (EF Core), Repositórios, Mapeamentos
└── 📂 NexusPDV.API             # Controllers, Configurações, Swagger
Destaques Técnicos
Rich Domain Models: As entidades não são anêmicas. A lógica de "Baixar Estoque" reside dentro da entidade Product, protegendo o estado do objeto.

Transaction Management: Uso de Unit of Work para garantir atomicidade entre tabelas Orders, OrderItems e Products.

Fail-Fast Validation: Validação de inputs (via FluentValidation) antes de atingir o banco de dados.

⚙️ Como Rodar o Projeto
Pré-requisitos
.NET SDK 9.0

SQL Server (Express, Developer ou Docker)

Passo a Passo
Clone o repositório:

Bash

git clone https://github.com/JulioNogueira99/NexusPDV.git
cd NexusPDV
Configure o Banco de Dados: No arquivo NexusPDV.API/appsettings.json, ajuste a ConnectionString para o seu SQL Server local:

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=NexusPDV;Trusted_Connection=True;TrustServerCertificate=True;"
}
Execute as Migrations: Abra o terminal na raiz e execute:

Bash

dotnet ef database update -p NexusPDV.Infrastructure -s NexusPDV.API
Popule o Banco (Seed Inicial): Como a API foca no processo de Venda, execute este script no seu SQL Server para criar os dados base (Cliente e Produtos):

SQL

INSERT INTO Customers (Name, Email, Cpf, CreatedAt) VALUES ('Visitante', 'teste@nexus.com', '11122233344', GETDATE());
INSERT INTO Products (Title, Price, StockQuantity, CreatedAt) VALUES ('Notebook Gamer', 5000.00, 10, GETDATE());
INSERT INTO Products (Title, Price, StockQuantity, CreatedAt) VALUES ('Mouse Sem Fio', 50.00, 5, GETDATE());
Execute a API:

Bash

dotnet run --project NexusPDV.API
Acesse o Swagger em: https://localhost:7193/swagger (ou a porta indicada no terminal).

🧪 Rodando os Testes
O projeto conta com testes unitários cobrindo o Caso de Uso de Vendas, validando cenários de sucesso e falha (ex: estoque insuficiente).

Para rodar os testes:

Bash

dotnet test
🔌 Endpoints Principais
Orders
POST /api/Orders - Realiza uma nova venda (baixa estoque automaticamente).

Body Exemplo:

JSON

{
  "customerId": 1,
  "items": [
    { "productId": 1, "quantity": 1 }
  ]
}
GET /api/Orders/{id} - Consulta um pedido e seus itens.

🤝 Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir Issues ou Pull Requests.

📝 Licença
Este projeto está sob a licença MIT.

Desenvolvido com 💜 por Júlio.
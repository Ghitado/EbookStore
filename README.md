# EbookStore

Plataforma de e-commerce para compra e venda de eBooks, desenvolvida em .NET 8, seguindo os princípios de Clean Architecture e DDD (Domain-Driven Design).  
O projeto oferece uma estrutura organizada e escalável, com separação clara entre as camadas Domain, Application, Infrastructure e API.

## Arquitetura

A aplicação segue uma estrutura baseada em Clean Architecture:

EbookStore/  
├── EbookStore.Domain/          Entidades, Value Objects, Regras de Negócio, Interfaces  
├── EbookStore.Application/     Casos de uso, DTOs, Validações  
├── EbookStore.Infrastructure/  Repositórios, Persistência (EF Core), Serviços externos  
├── EbookStore.API/             Endpoints, Controllers, Autenticação, Swagger  
├── EbookStore.Domain.Tests/    Testes unitários (xUnit + FluentAssertions)

## Tecnologias principais

- .NET 8  
- Entity Framework Core 8  
- Azure Blob Storage  
- JWT Authentication  
- Swagger / OpenAPI  
- xUnit e FluentAssertions para testes  
- Clean Architecture + DDD

## Como executar localmente

Pré-requisitos:  
- .NET SDK 8.0 ou superior  
- SQL Server ou Docker  
- Azure Storage (opcional)

Passos:

```bash
git clone https://github.com/seuusuario/EbookStore.git
cd EbookStore
dotnet restore
dotnet ef database update --project EbookStore.Infrastructure --startup-project EbookStore.API
dotnet run --project EbookStore.API
```

## Executando a API

A API ficará disponível em:  
http://localhost:5000/swagger

## Testes

Para executar os testes unitários:

```bash
dotnet test
```

Os testes cobrem as entidades do domínio, garantindo a integridade das regras de negócio.

## Licença

Este projeto está licenciado sob a MIT License.

## Contato

Desenvolvido por Thiago de Melo Mota
E-mail: thiagodemelomota@gmail.com

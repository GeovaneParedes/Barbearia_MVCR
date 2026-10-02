# Barbearia MVC

Sistema de gerenciamento de barbearia desenvolvido para treinamento em **ASP.NET Core MVC**, **Entity Framework Core** e **Repository Pattern**.

## Tecnologias utilizadas

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=asp.net&logoColor=white)
![MVC](https://img.shields.io/badge/Architecture-MVC-orange?style=for-the-badge)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

## Objetivo

O projeto simula um sistema para administração de uma barbearia, permitindo organizar:

- Agendamentos;
- Barbeiros;
- Clientes;
- Serviços;
- Pagamentos.

## Padrões utilizados

- **MVC — Model-View-Controller**
- **Repository Pattern**
- **Injeção de Dependência**
- **Entity Framework Core**
- **Migrations para versionamento do banco de dados**

## Estrutura do projeto

```text
Barbearia_MVC/
├── Controllers/
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
├── Repositories/
│   ├── Appointment/
│   ├── Barber/
│   ├── Customer/
│   ├── Payment/
│   └── Service/
├── Views/
├── wwwroot/
├── appsettings.json
├── Program.cs
└── Barbearia_MVC.csproj
```

## Bibliotecas principais

| Biblioteca | Finalidade |
|---|---|
| `Microsoft.AspNetCore.Mvc` | Implementação do padrão MVC |
| `Microsoft.EntityFrameworkCore` | Mapeamento objeto-relacional |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | Integração do Entity Framework Core com PostgreSQL |
| `Microsoft.EntityFrameworkCore.Tools` | Criação e gerenciamento de migrations |

## Requisitos

- .NET SDK instalado;
- PostgreSQL instalado e em execução;
- Visual Studio Code ou Visual Studio;
- Ferramenta `dotnet-ef`.

## Configuração do banco de dados

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=barbearia_db;Username=postgres;Password=SUA_SENHA"
  }
}
```

## Instalação

Clone o projeto e acesse a pasta:

```bash
git clone https://github.com/GeovaneParedes/Barbearia_MVCR.git
cd Barbearia_MVC/Barbearia_MVC
```

Restaure as dependências:

```bash
dotnet restore
```

Instale a ferramenta do Entity Framework Core, caso necessário:

```bash
dotnet tool install --global dotnet-ef
```

## Banco de dados e migrations

Crie a migration inicial:

```bash
dotnet ef migrations add InitialCreate
```

Atualize o banco de dados:

```bash
dotnet ef database update
```

## Executando o projeto

```bash
dotnet run
```

Depois, abra no navegador o endereço apresentado pelo terminal, normalmente:

```text
https://localhost:5001
```

A rota inicial está configurada para abrir o controlador `Appointment`:

```text
/Appointment/Index
```

## Injeção de dependência

Os repositórios são registrados no `Program.cs` com escopo por requisição:

```csharp
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
```

## Licença

Projeto desenvolvido para fins educacionais e treinamento em arquitetura MVC com .NET.
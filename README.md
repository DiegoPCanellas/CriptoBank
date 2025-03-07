Bank Transactions API - Ethereum Simulation

Overview

This project is an API for bank transactions that simulates Ethereum value in real-time. It was developed using C# and .NET, following best practices such as Clean Architecture and Dependency Injection. The API provides a secure and scalable environment for handling banking transactions, ensuring authentication via JWT and data persistence through Entity Framework.

Features

Error Handling Middleware: A custom middleware to handle request errors and provide standardized responses.

JWT Authentication: Secure authentication using JSON Web Tokens.

Clean Architecture: A modular structure ensuring separation of concerns.

Automapper Integration: Automatic mapping between domain models and DTOs.

Entity Framework Core: ORM for database interactions and migrations.

Constructor Dependency Injection: Services are injected through constructors for better maintainability and testability.

Project Structure

The project follows Clean Architecture, which separates concerns into different layers:

📂 src
 ├── 📂 Application  # Business logic and use cases
 │   ├── Services   # Service layer for business logic
 │   ├── DTOs       # Data Transfer Objects for input/output
 │   ├── Interfaces # Contracts for repositories and services
 │
 ├── 📂 Domain      # Core entities and business rules
 │   ├── Entities   # Domain models representing database tables
 │   ├── Enums      # Enumerations used across the domain
 │
 ├── 📂 Infrastructure # Data access and external integrations
 │   ├── Persistence  # Entity Framework context and repositories
 │   ├── Mappings     # AutoMapper configurations
 │   ├── JWT          # JWT Authentication implementation
 │
 ├── 📂 API         # Entry point of the application (Controllers, Middleware)
 │   ├── Controllers  # Exposes endpoints to the clients
 │   ├── Middleware   # Global error handling and logging
 │   ├── Program.cs   # Configures DI, services, and middleware

Key Components

1. Error Handling Middleware

A custom middleware intercepts requests, handling exceptions globally and returning standardized responses.

  public class UnauthorizedMiddleware
  {
      private readonly RequestDelegate _next;

      public UnauthorizedMiddleware(RequestDelegate next)
      {
          _next = next;
      }

      public async Task Invoke(HttpContext httpContext)
      {
          await _next(httpContext);

          if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
          {
              httpContext.Response.ContentType = "application/json";
              var result = System.Text.Json.JsonSerializer.Serialize(new { message = $"Acesso negado. Código: {(int)HttpStatusCode.Unauthorized}",
                                                                           success = false});
              await httpContext.Response.WriteAsync(result);
          }
      }
  }

  public static class MiddlewareExtensions
  {
      public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
      {
          return builder.UseMiddleware<UnauthorizedMiddleware>();
      }
  }

2. JWT Authentication

Implemented JWT authentication to ensure secure access to API endpoints.

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "CriptoBank",
            ValidAudience = "CriptoBank",
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero 
        };
    });

3. AutoMapper for DTO Mapping

Used AutoMapper to convert domain models into DTOs, avoiding exposure of internal models.

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, TransactionDTO>();
        CreateMap<TransactionDTO, Transaction>();
    }
}

4. Entity Framework & Migrations

Used Entity Framework Core for data persistence, applying migrations automatically.

public class ApplicationDbContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

# Apply database migrations
dotnet ef migrations add InitialCreate

dotnet ef database update

5. Constructor Dependency Injection

All services are injected via constructor dependency injection, ensuring loose coupling.

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
}

Running the API

Prerequisites

.NET SDK 7.0+

SQL Server or PostgreSQL (configured in appsettings.json)

Postman or cURL for testing endpoints

Installation & Execution

Clone the repository:

git clone https://github.com/yourusername/BankTransactionsAPI.git
cd BankTransactionsAPI

Restore dependencies:

dotnet restore

Run database migrations:

dotnet ef database update

Start the application:

dotnet run

Future Enhancements

Implement caching for frequent queries

Integrate real-time Ethereum pricing API

Add role-based authorization

Author

Developed by Diego Paredes as part of a Computer Science TCC Project.


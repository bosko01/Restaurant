# Restaurant Management API

## Project Description

Restaurant Management API is a **RESTful API** developed in **C# using ASP.NET Core**, designed to manage restaurants, their contact information, and working hours. The project is built according to **Clean Architecture** principles, utilizing **CQRS** and **Unit of Work** patterns, aiming to create a maintainable, scalable, and testable backend solution.

## The API provides:  
- Create, Read, Update, Delete (CRUD) operations for restaurants  
- Management of contact information, including email and phone number  
- Management of restaurant working hours  
- Centralized error handling using middleware  

## Technologies and Tools
- **C# / .NET 8** – primary programming language and framework  
- **ASP.NET Core Web API** – building RESTful services  
- **Entity Framework Core (EF Core)** – ORM for database access  
- **SQL Server** – relational database  
- **Swashbuckle / Swagger** – API documentation and testing  
- **Mapster** – mapping between DTOs and entities  
- **Dependency Injection (DI)** – managing service dependencies  
- **Middleware** – centralized error handling and logging  
- **Fluent API** – configuring EF Core entities  
- **Git** – version control  

## Architecture
The project follows **Clean Architecture**:
/Api -> ASP.NET Core Web API, entry point of the application
/Application -> Business logic, DTOs, CQRS commands and queries
/Domain -> Entities and business rules
/Infrastructure -> Database access, repository implementations


### Key Principles
- **Clean Architecture** – separation of layers for maintainability and testability  
- **CQRS (Command Query Responsibility Segregation)** – separating read and write operations  
- **Unit of Work & Repository Pattern** – centralized transaction management and data access  
- **DTOs (Data Transfer Objects)** – encapsulating data between layers  
- **Middleware for Error Handling** – centralized exception handling and logging  

### Best Practices Implemented
Data validation using annotations like [EmailAddress] and [Required]
Automatic mapping of entities and DTOs via Mapster
Centralized error handling using custom middleware
Swagger UI for exploring and testing API endpoints
Environment-based configuration (appsettings.Development.json, appsettings.Production.json)
Dependency Injection for all services and repositories

### Future Enhancements
User authentication and authorization (JWT)
Advanced filtering and pagination for restaurants
Unit testing for services and CQRS commands
Docker support for deployment

## Contact
  **Email**: vujanovicbosko01@gmail.com
  
  **LinkedIn**: https://www.linkedin.com/in/bosko-vujanovic

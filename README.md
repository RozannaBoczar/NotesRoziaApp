# NotesApp – Backend

A RESTful API for managing notes, built with **ASP.NET Core Web API**, **.NET 10**, **Entity Framework Core**, and **PostgreSQL**.

The project is designed to demonstrate clean architecture principles, separation of concerns, dependency injection, database integration, and automated testing.

## Features

- Create, read, update, and delete notes (CRUD).
- RESTful API endpoints.
- PostgreSQL database integration.
- Entity Framework Core for data access.
- Database schema management using EF Core migrations.
- Swagger UI for API documentation and testing.
- Unit tests using xUnit and Moq.
- Integration tests using WebApplicationFactory and Testcontainers.

## Tech Stack

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 10 | Application framework |
| ASP.NET Core Web API | REST API |
| Entity Framework Core | ORM |
| PostgreSQL | Relational database |
| xUnit | Unit and integration testing |
| Moq | Mocking dependencies |
| Bogus | Test data generation |
| WebApplicationFactory | API integration testing |
| Testcontainers | PostgreSQL test database in Docker |
| Swagger / OpenAPI | API documentation |

## Architecture

The solution follows a layered architecture inspired by Clean Architecture principles.

```text
NotesApp
│
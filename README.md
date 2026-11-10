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
├── NotesApp.Api
│   ├── Controllers
│   └── Program.cs
│
├── NotesApp.Application
│   ├── Abstractions
│   └── Notes
│       ├── CreateNote
│       ├── GetNotes
│       ├── GetNoteById
│       ├── UpdateNote
│       └── DeleteNote
│
├── NotesApp.Domain
│   └── Entities
│       └── Note.cs
│
├── NotesApp.Infrastructure
│   └── Persistence
│       └── NotesDbContext.cs
│
├── NotesApp.UnitTests
│
└── NotesApp.IntegrationTests
```

### Project responsibilities

- **NotesApp.Api** – Exposes HTTP endpoints and handles incoming requests.
- **NotesApp.Application** – Contains application logic, request handlers, and abstractions.
- **NotesApp.Domain** – Contains core domain entities and business rules.
- **NotesApp.Infrastructure** – Implements data access and database integration.
- **NotesApp.UnitTests** – Tests application logic in isolation.
- **NotesApp.IntegrationTests** – Tests API behavior and database interactions using a real PostgreSQL instance in a Docker container.

## API Endpoints

Base URL: `/api/Notes`

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Notes` | Retrieve all notes |
| GET | `/api/Notes/{id}` | Retrieve a note by ID |
| POST | `/api/Notes` | Create a new note |
| PUT | `/api/Notes/{id}` | Update an existing note |
| DELETE | `/api/Notes/{id}` | Delete a note |

### Example: Create a note

**Request**

```http
POST /api/Notes
Content-Type: application/json
```

```json
{
  "title": "My first note",
  "content": "This is the content of my first note."
}
```

**Response**

```http
201 Created
```

The response contains the created note, including its generated ID and timestamps.

## Getting Started

### Prerequisites

Make sure you have installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) – required for container-based integration tests.
- Visual Studio or Visual Studio Code.

### 1. Clone the repository

```bash
git clone <repository-url>
cd NotesApp
```

### 2. Configure the database connection

Create a PostgreSQL database named `NotesAppDb`.

Configure the connection string using .NET User Secrets:

```bash
dotnet user-secrets init --project NotesApp.Api
```

Then set the connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=NotesAppDb;Username=YOUR_USERNAME;Password=YOUR_PASSWORD" --project NotesApp.Api
```

Replace the username and password with your local PostgreSQL credentials.

**Important:** Never commit database credentials or other secrets to source control.

### 3. Apply database migrations

From the solution directory, run:

```bash
dotnet ef database update --project NotesApp.Infrastructure --startup-project NotesApp.Api
```

### 4. Run the API

```bash
dotnet run --project NotesApp.Api
```

Open the Swagger UI using the URL displayed in the terminal, followed by `/swagger`.

## Testing

The solution contains two types of automated tests.

### Unit Tests

Unit tests verify application logic independently of external dependencies.

Technologies:
- xUnit
- Moq
- Bogus

Run unit tests:

```bash
dotnet test NotesApp.UnitTests
```

### Integration Tests

Integration tests verify API behavior and database interactions.

They use:
- WebApplicationFactory
- Testcontainers
- PostgreSQL running in Docker

Docker Desktop must be running before executing integration tests.

Run integration tests:

```bash
dotnet test NotesApp.IntegrationTests
```

Run all tests:

```bash
dotnet test
```

## Development Roadmap

Planned improvements:

- React + TypeScript frontend.
- End-to-end tests using Playwright.
- Automated CI/CD pipeline with GitHub Actions.
- Docker Compose configuration for running the application and database.
- AI-powered note summarization.
- Additional validation and error handling.

## License

This project is currently intended for learning and portfolio purposes.
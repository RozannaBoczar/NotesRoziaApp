# NotesRoziaApp

NotesRoziaApp is a layered ASP.NET Core Web API for creating, reading, updating, and deleting notes.

## Architecture

The solution is divided into the following projects:

- **NotesApp.Api** — ASP.NET Core Web API, controllers, dependency injection, and application configuration.
- **NotesApp.Application** — application use cases, request models, handlers, and repository abstractions.
- **NotesApp.Domain** — domain entities and core business models.
- **NotesApp.Infrastructure** — Entity Framework Core persistence, SQLite configuration, and repository implementations.
- **NotesApp.UnitTests** — unit tests for application handlers.
- **NotesApp.IntegrationTests** — HTTP API integration tests.

## Requirements

- .NET 10 SDK
- Visual Studio 2026 or another IDE with .NET 10 support

## Running the application

From the repository root, run:

```powershell
dotnet restore .\NotesRoziaApp.slnx
dotnet run --project .\NotesApp.Api\NotesApp.Api.csproj
```

The API uses SQLite with the database connection configured in `NotesApp.Api/appsettings.json`. The database file is created automatically when the application starts.

## API endpoints

The API is available under `/api/notes`:

| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/notes` | Get all notes |
| GET | `/api/notes/{id}` | Get a note by identifier |
| POST | `/api/notes` | Create a note |
| PUT | `/api/notes/{id}` | Update a note |
| DELETE | `/api/notes/{id}` | Delete a note |

Example request body for creating a note:

```json
{
  "title": "My note",
  "content": "Note content"
}
```

## Building the solution

```powershell
dotnet build .\NotesRoziaApp.slnx
```

## Running tests

Run all tests with:

```powershell
dotnet test .\NotesRoziaApp.slnx
```

The test projects contain unit tests for note handlers and integration tests for the HTTP API.

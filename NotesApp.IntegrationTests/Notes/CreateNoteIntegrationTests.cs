using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NotesApp.Application.Notes.CreateNote;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Persistence;
using NotesApp.IntegrationTests.Fixtures;
using System.Net.Http.Json;
using System.Reflection;
using Xunit;

namespace NotesApp.IntegrationTests.Notes;

public class CreateNoteIntegrationTests
    : IClassFixture<PostgreSqlContainerFixture>
{
    private readonly PostgreSqlContainerFixture _database;

    public CreateNoteIntegrationTests(PostgreSqlContainerFixture database)
    {
        _database = database;
    }

    [Fact]
    public async Task Api_Should_CreateNote()
    {
        // Arrange
        await using var factory = new NotesApiFactory(_database.ConnectionString);
        using var client = factory.CreateClient();

        await using var scope = factory.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<NotesDbContext>();

        await dbContext.Database.MigrateAsync();

        var faker = new Faker();
        var title = faker.Lorem.Sentence();
        var content = faker.Lorem.Paragraph(20);
        var request = new CreateNoteRequest(title, content);

        // Act
        var response = await client.PostAsJsonAsync("/api/Notes", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdNote = await response.Content.ReadFromJsonAsync<Note>();

        Assert.NotNull(createdNote);

        var noteFromDatabase = await dbContext.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == createdNote.Id);

        Assert.NotNull(noteFromDatabase);
        Assert.Equal(title, noteFromDatabase.Title);
        Assert.Equal(content, noteFromDatabase.Content);
    }
}
using Testcontainers.PostgreSql;
using Xunit;

namespace NotesApp.IntegrationTests.Fixtures;

public sealed class PostgreSqlContainerFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container =
        new PostgreSqlBuilder()
            .WithImage("postgres:17-alpine")
            .WithDatabase("NotesAppTestDb")
            .WithUsername("postgres")
            .WithPassword("rozia")
            .Build();

    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NotesApp.Infrastructure.Persistence;

namespace NotesApp.IntegrationTests.Fixtures;

public class NotesApiFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public NotesApiFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<NotesDbContext>>();

            services.AddDbContext<NotesDbContext>(options =>
                options.UseNpgsql(_connectionString));


        });
    }
}
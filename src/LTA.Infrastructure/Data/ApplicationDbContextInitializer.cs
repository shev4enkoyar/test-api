using LTA.Application.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LTA.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILoggerAdapter<ApplicationDbContextInitializer> _logger;

    public ApplicationDbContextInitializer(ILoggerAdapter<ApplicationDbContextInitializer> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _dbContext.Database.EnsureCreatedAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred on database initialization.");
            throw;
        }
    }
}
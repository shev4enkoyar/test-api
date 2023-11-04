using LTA.Application.Common.Interfaces;
using LTA.Infrastructure.Data;
using LTA.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LTA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        var databaseContainerName = Environment.GetEnvironmentVariable("POSTGRES_CONTAINER_NAME");
        var databaseName = Environment.GetEnvironmentVariable("POSTGRES_DB");
        var databaseUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
        var databasePassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

        services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

        services.AddSingleton<IJsonPlaceholderApiClient, JsonPlaceholderApiClient>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql($"Host={databaseContainerName};" +
                              $"Database={databaseName};" +
                              $"User Id={databaseUser};" +
                              $"Password={databasePassword}");
        });

        services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }
}
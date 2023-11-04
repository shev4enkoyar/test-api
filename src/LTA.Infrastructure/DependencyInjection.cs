using LTA.Application.Common.Interfaces;
using LTA.Domain.Common;
using LTA.Infrastructure.Data;
using LTA.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LTA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ApplicationSettings applicationSettings)
    {
        services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

        services.AddSingleton<IJsonPlaceholderApiClient, JsonPlaceholderApiClient>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                options.UseNpgsql($"Host={applicationSettings.DatabaseAddress};" +
                                  $"Database={applicationSettings.DatabaseName};" +
                                  $"User Id={applicationSettings.DatabaseUser};" +
                                  $"Password={applicationSettings.DatabasePassword};" +
                                  $"Port={applicationSettings.DatabasePort}");
            }
            else
            {
                options.UseNpgsql(
                    $"User ID={applicationSettings.DatabaseUser};" +
                    $"Password={applicationSettings.DatabasePassword};" +
                    $"Server={applicationSettings.DatabaseAddress};" +
                    $"Port={applicationSettings.DatabasePort};" +
                    $"Database={applicationSettings.DatabaseName};" +
                    "Integrated Security=true;" +
                    "Pooling=true;");
            }
        });

        services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }
}
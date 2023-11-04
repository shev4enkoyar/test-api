using LTA.Application;
using LTA.Domain.Common;
using LTA.Infrastructure;
using LTA.Infrastructure.Data;
using LTA.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Contrib.WaitAndRetry;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection(nameof(ApplicationSettings)));

var config = builder.Configuration.GetSection(nameof(ApplicationSettings))
    .Get<ApplicationSettings>();

builder.Services.AddInfrastructureServices(config);
builder.Services.AddApplicationServices();

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default",
        new CacheProfile
        {
            Duration = config.DefaultCacheDuration
        });
});

builder.Services.AddResponseCaching();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(JsonPlaceholderApiClient.ClientName,
        client => { client.BaseAddress = new Uri(config.JsonPlaceholderApiUrl); })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(
            TimeSpan.FromSeconds(config.JsonPlaceholderApiDelayInSec),
            config.JsonPlaceholderApiNumRetry)));

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
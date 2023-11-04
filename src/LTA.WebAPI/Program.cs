using LTA.Application;
using LTA.Infrastructure;
using LTA.Infrastructure.Data;
using LTA.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Contrib.WaitAndRetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default5",
        new CacheProfile
        {
            Duration = 5
        });
});

builder.Services.AddResponseCaching();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(JsonPlaceholderApiClient.ClientName,
        client => { client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"); })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
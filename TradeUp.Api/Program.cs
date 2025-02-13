using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tradeup.Domain.Core.Bus;
using TradeUp.Api.Extensions;
using TradeUp.Application;
using TradeUp.Application.Configuration;
using TradeUp.Application.EventHandlers;
using TradeUp.Application.Abstractions.Messaging;
using TradeUp.Domain.Core.Events;
using TradeUp.Infrastructure;
using TradeUp.Infrastructure.IoC;
using TradeUp.Infrastructure.Services;
using static System.Net.WebRequestMethods;
using TradeUp.Application.Abstractions;
using Serilog;
using TradeUp.Application.Abstractions.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Dapper;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using TradeUp.Api.OpenApi;
using Asp.Versioning.ApiExplorer;
using TradeUp.BackgroundServices;
using TradeUp.Infrastructure.Websockets;
using TradeUp.Infrastructure.Options;
using TradeUp.Shared.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true) // allow any origin
       .AllowCredentials().Build();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddHttpClient<IStockApiClient, FinnHubApiClient>(client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "TradeUp"); // Optional, but good practice for APIs

});
builder.Services.Configure<FinnHubConfiguration>(builder.Configuration.GetSection("FinnHub"));

RegisterServices(builder.Services, builder.Configuration);

void RegisterServices(IServiceCollection services, IConfiguration configuration)
{
    DependencyContainer.RegisterServices(services, configuration);
    builder.Services.RegisterApplicationServices();
    builder.Services.RegisterInfrastructureServices(configuration);

}

builder.Services.AddSignalR();

builder.Services.AddSingleton<FinnhubWebSocketClient>();

builder.Services.AddSingleton<StockPriceListener>();

builder.Services.AddHostedService<StockPriceListener>();

builder.Services.Configure<FinnhubOptions>(builder.Configuration.GetSection("FinnhubSettings"));
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in app.DescribeApiVersions())
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.ApplyMigrations();
    app.UseCors("EnableCORS");
    app.SeedData();
}

ConfigureEventBus(app);

void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<PriceThresholdReachedEvent, PriceThresholdReachedEventHandler>();
}

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<StockHub>("/stockHub");

app.MapControllers();


app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

//Workaround for integration test entry point
//TODO: Do proper endpoint entry for integration test
public partial class Program;

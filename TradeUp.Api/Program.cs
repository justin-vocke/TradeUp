using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tradeup.Domain.Core.Bus;
using TradeUp.Application;
using TradeUp.Application.Configuration;
using TradeUp.Application.EventHandlers;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Events;
using TradeUp.Infrastructure;
using TradeUp.Infrastructure.IoC;
using TradeUp.Infrastructure.Services;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IStockApiClient, StockApiClient>(client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "TradeUp"); // Optional, but good practice for APIs
    
});
builder.Services.Configure<AlphaVantageConfiguration>(builder.Configuration.GetSection("AlphaVantage"));


RegisterServices(builder.Services, builder.Configuration);

void RegisterServices(IServiceCollection services, IConfiguration configuration)
{
    DependencyContainer.RegisterServices(services);
    builder.Services.RegisterApplicationServices();
    builder.Services.RegisterInfrastructureServices(configuration);

}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureEventBus(app);

void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<PriceThresholdReachedEvent, PriceThresholdReachedEventHandler>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

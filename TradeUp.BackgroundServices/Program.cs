using Microsoft.AspNetCore.SignalR;
using TradeUp.BackgroundServices;
using TradeUp.Infrastructure.Options;
using TradeUp.Infrastructure.Websockets;
using TradeUp.Shared.Hubs;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true);
builder.Services.Configure<FinnhubOptions>(builder.Configuration.GetSection("FinnhubSettings"));

// Register WebSocket client
builder.Services.AddSingleton<IStockWebSocketClient, FinnhubWebSocketClient>();

builder.Services.AddHostedService<StockPriceListener>();

var host = builder.Build();
host.Run();

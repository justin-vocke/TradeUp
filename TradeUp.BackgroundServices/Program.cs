using TradeUp.BackgroundServices;
using TradeUp.Infrastructure.Options;
using TradeUp.Infrastructure.Websockets;

var builder = Host.CreateApplicationBuilder(args);
// Bind FinnhubOptions from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.Configure<FinnhubOptions>(builder.Configuration.GetSection("FinnhubSettings"));

// Register WebSocket client
builder.Services.AddSingleton<IStockWebSocketClient, FinnhubWebSocketClient>();

// Register the background worker
builder.Services.AddHostedService<StockPriceListener>();

var host = builder.Build();
host.Run();

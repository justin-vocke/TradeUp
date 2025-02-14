using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions;

namespace TradeUp.Infrastructure.Websockets
{
    public class StockWebSocketSubscriptionService : IStockWebSocketSubscriptionService
    {
        private readonly FinnhubWebSocketClient _finnhubWebSocketClient;
        private readonly ILogger<StockWebSocketSubscriptionService> _logger;

        public StockWebSocketSubscriptionService(FinnhubWebSocketClient finnhubWebSocketClient, ILogger<StockWebSocketSubscriptionService> logger)
        {
            _finnhubWebSocketClient = finnhubWebSocketClient;
            _logger = logger;
        }

        public async Task SubscribeToStockAsync(string stockSymbol)
        {
            _logger.LogInformation($"Subscribing to {stockSymbol}...");
            await _finnhubWebSocketClient.SubscribeAsync(stockSymbol);
        }

        public async Task UnsubscribeFromStockAsync(string stockSymbol)
        {
            _logger.LogInformation($"Unsubscribing from {stockSymbol}...");
            await _finnhubWebSocketClient.UnsubscribeAsync(stockSymbol);
        }
    }
}

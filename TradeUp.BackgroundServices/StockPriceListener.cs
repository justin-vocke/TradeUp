using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions;
using TradeUp.Application.DTO.FinnHub;
using TradeUp.Infrastructure.Websockets;
using TradeUp.Shared.Hubs;

namespace TradeUp.BackgroundServices
{
    public class StockPriceListener : BackgroundService
    {
        private readonly IHubContext<StockHub> _hubContext;
        private readonly FinnhubWebSocketClient _finnhubWebSocketClient;
        private readonly ILogger<StockPriceListener> _logger;

        public StockPriceListener(IHubContext<StockHub> hubContext, FinnhubWebSocketClient finnhubWebSocketClient, ILogger<StockPriceListener> logger)
        {
            _hubContext = hubContext;
            _finnhubWebSocketClient = finnhubWebSocketClient;
            _logger = logger;
            _finnhubWebSocketClient.OnMessageReceived += HandleStockUpdate;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StockPriceListener service started.");
            await _finnhubWebSocketClient.ConnectAsync(stoppingToken);
        }

        private void HandleStockUpdate(object? sender, string jsonMessage)
        {
            try
            {
                var stockData = JsonSerializer.Deserialize<FinnhubWebSocketResponse>(jsonMessage);
                if (stockData?.Type == "trade" && stockData.Data != null)
                {
                    _ = ProcessStockUpdateAsync(stockData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing stock update: {ex.Message}");
            }
        }

        private async Task ProcessStockUpdateAsync(FinnhubWebSocketResponse websocketResponse)
        {
            foreach (var trade in websocketResponse.Data)
            {
                await _hubContext.Clients.Group(trade.Symbol).SendAsync("ReceiveStockUpdate", trade);
                _logger.LogInformation($"Sent stock update to group {trade.Symbol}: {trade.LastPrice}");
            }
        }
    }
}

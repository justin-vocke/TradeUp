using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions;
using TradeUp.Shared.Hubs;

namespace TradeUp.BackgroundServices
{
    public class StockPriceListener : BackgroundService
    {
        private readonly IStockService _stockService;
        private readonly IHubContext<StockHub> _hubContext;

        public StockPriceListener(IStockService stockService, IHubContext<StockHub> hubContext)
        {
            _stockService = stockService;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var stockUpdate in _stockService.GetStockUpdates(stoppingToken))
            {
                await _hubContext.Clients.Group(stockUpdate.Ticker).SendAsync("ReceiveStockUpdate", stockUpdate);
            }
        }
    }
}

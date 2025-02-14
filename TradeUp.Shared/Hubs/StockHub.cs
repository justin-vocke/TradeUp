using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions;

namespace TradeUp.Shared.Hubs
{
    public class StockHub : Hub
    {
        private readonly IStockWebSocketSubscriptionService _stockSubscriptionService;

        public StockHub(IStockWebSocketSubscriptionService stockSubscriptionService)
        {
            _stockSubscriptionService = stockSubscriptionService;
        }
        public async Task SubscribeToStock(string stockSymbol)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, stockSymbol);
            await _stockSubscriptionService.SubscribeToStockAsync(stockSymbol);

        }

        public async Task UnsubscribeFromStock(string stockSymbol)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, stockSymbol);
            await _stockSubscriptionService.UnsubscribeFromStockAsync(stockSymbol);

        }
    }
}

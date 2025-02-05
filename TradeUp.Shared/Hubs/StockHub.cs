using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TradeUp.Shared.Hubs
{
    public class StockHub : Hub
    {
        public async Task SubscribeToStock(string stockSymbol)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, stockSymbol);
        }

        public async Task UnsubscribeFromStock(string stockSymbol)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, stockSymbol);
        }
    }
}

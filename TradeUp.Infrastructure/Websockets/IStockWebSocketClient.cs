using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Infrastructure.Websockets
{
    public interface IStockWebSocketClient
    {
        event EventHandler<string>? OnMessageReceived;
        event EventHandler? OnConnected;
        event EventHandler? OnDisconnected;

        Task ConnectAsync(CancellationToken cancellationToken = default);
        Task DisconnectAsync();
        Task SubscribeAsync(string symbol);
        Task UnsubscribeAsync(string symbol);
    }
}

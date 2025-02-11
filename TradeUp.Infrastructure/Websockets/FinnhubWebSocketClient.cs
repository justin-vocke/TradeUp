using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradeUp.Infrastructure.Options;

namespace TradeUp.Infrastructure.Websockets
{
    public class FinnhubWebSocketClient : IStockWebSocketClient, IDisposable
    {
        private readonly ClientWebSocket _webSocket = new();
        private readonly Uri _uri;
        private CancellationTokenSource? _cts;

        public event EventHandler<string>? OnMessageReceived;
        public event EventHandler? OnConnected;
        public event EventHandler? OnDisconnected;

        public FinnhubWebSocketClient(IOptions<FinnhubOptions> options)
        {
            var apiKey = options.Value.ApiKey;
            _uri = new Uri($"wss://ws.finnhub.io?token={apiKey}");
        }
        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await _webSocket.ConnectAsync(_uri, _cts.Token);
            OnConnected?.Invoke(this, EventArgs.Empty);

            _ = ReceiveMessagesAsync(_cts.Token); // Start listening
        }

        public async Task DisconnectAsync()
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                OnDisconnected?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task SubscribeAsync(string symbol)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                var message = JsonSerializer.Serialize(new { type = "subscribe", symbol });
                await _webSocket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task UnsubscribeAsync(string symbol)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                var message = JsonSerializer.Serialize(new { type = "unsubscribe", symbol });
                await _webSocket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4];

            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                if (result.MessageType == WebSocketMessageType.Close) break;

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                OnMessageReceived?.Invoke(this, message);
            }
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _webSocket.Dispose();
        }
    }
}

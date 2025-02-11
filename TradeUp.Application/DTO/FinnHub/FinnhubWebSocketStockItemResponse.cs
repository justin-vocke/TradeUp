using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.FinnHub
{
    public class FinnhubWebSocketStockItemResponse : IStockApiResponse
    {
        [JsonPropertyName("s")]
        public string Symbol { get; set; }
        [JsonPropertyName("t")]
        public long TimeStamp { get; set; }
        [JsonPropertyName("p")]
        public long LastPrice { get; set; }
        [JsonPropertyName("v")]
        public long Volume { get; set; }

        // Convert the Unix timestamp to DateTime after deserialization
        [JsonIgnore] // We don't need to serialize this, just the timestamp.
        public DateTime Time => DateTimeOffset.FromUnixTimeSeconds(TimeStamp).UtcDateTime;
    }
}

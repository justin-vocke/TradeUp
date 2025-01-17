using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.FinnHub
{
    public class FinnHubStockApiResponse : IStockApiResponse
    {
        [JsonPropertyName("c")]
        public decimal Current { get; set; }
        [JsonPropertyName("h")]
        public decimal High { get; set; }
        [JsonPropertyName("dp")]
        public decimal PercentChange { get; set; }

        [JsonPropertyName("l")]
        public decimal Low { get; set; }

        [JsonPropertyName("o")]
        public decimal OpeningPrice { get; set; }

        [JsonPropertyName("pc")]
        public decimal PreviousClose { get; set; }

        [JsonPropertyName("t")]
        public long TimeStamp { get; set; }

        // Convert the Unix timestamp to DateTime after deserialization
        [JsonIgnore] // We don't need to serialize this, just the timestamp.
        public DateTime Time => DateTimeOffset.FromUnixTimeSeconds(TimeStamp).UtcDateTime;
    }
}

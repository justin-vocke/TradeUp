using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.AlphaVantage
{
    public class AlphaVantageApiResponse : IStockApiResponse
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
        public long TimeStamp { get; set; } = default;
    }
}

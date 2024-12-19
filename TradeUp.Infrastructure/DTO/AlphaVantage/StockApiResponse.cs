using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradeUp.Infrastructure.DTO.AlphaVantage
{
    internal class StockApiResponse
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }
}

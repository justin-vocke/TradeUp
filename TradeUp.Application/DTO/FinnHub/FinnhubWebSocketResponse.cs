using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.FinnHub
{
    public class FinnhubWebSocketResponse
    {
        [JsonPropertyName("data")]
        public List<FinnhubWebSocketStockItemResponse> Data { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

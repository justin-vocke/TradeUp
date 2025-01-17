using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.FinnHub
{
    public class FinnHubStockDto : IStockApiResponseDto
    {
        public string Ticker { get; set; }
        public decimal Current { get; set; }
        public decimal High { get; set; }
        public decimal PercentChange { get; set; }
        public decimal Low { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal PreviousClose { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}

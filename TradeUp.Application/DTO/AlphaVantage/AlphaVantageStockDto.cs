using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.AlphaVantage
{
    public  class AlphaVantageStockDto : IStockApiResponseDto
    {
        public string Symbol { get; set; }

        public string Open { get; set; }

        public string High { get; set; }

        public string Low { get; set; }

        public string Price { get; set; }

        public string Volume { get; set; }

        public string LatestTradingDay { get; set; }

        public string PreviousClose { get; set; }

        public string Change { get; set; }

        public string ChangePercent { get; set; }

        public DateTime Time { get; set;  } = DateTime.Now;
    }
}

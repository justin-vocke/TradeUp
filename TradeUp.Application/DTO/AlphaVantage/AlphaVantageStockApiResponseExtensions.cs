using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.DTO.FinnHub;

namespace TradeUp.Application.DTO.AlphaVantage
{
    public static class AlphaVantageStockApiResponseExtensions
    {
        public static AlphaVantageStockDto ToDto(this AlphaVantageApiResponse apiResponse)
        {
            return new AlphaVantageStockDto
            {
                Symbol = apiResponse.GlobalQuote.Symbol,
                Open = apiResponse.GlobalQuote.Open,
                High = apiResponse.GlobalQuote.High,
                Low = apiResponse.GlobalQuote.Low,
                Price = apiResponse.GlobalQuote.Price,
                Volume = apiResponse.GlobalQuote.Volume,
                LatestTradingDay = apiResponse.GlobalQuote.LatestTradingDay,
                PreviousClose = apiResponse.GlobalQuote.PreviousClose,
                Change = apiResponse.GlobalQuote.Change,
                ChangePercent = apiResponse.GlobalQuote.ChangePercent
            };
        }
    }
}

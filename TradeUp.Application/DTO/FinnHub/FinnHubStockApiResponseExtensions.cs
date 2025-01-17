using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Application.DTO.FinnHub
{
    public static class FinnHubStockApiResponseExtensions
    {
        public static FinnHubStockDto ToDto(this FinnHubStockApiResponse apiResponse, string ticker)
        {
            return new FinnHubStockDto
            {
                Ticker = ticker,
                Current = apiResponse.Current,
                High = apiResponse.High,
                PercentChange = apiResponse.PercentChange,
                Low = apiResponse.Low,
                OpeningPrice = apiResponse.OpeningPrice,
                PreviousClose = apiResponse.PreviousClose,
                Time = apiResponse.Time
            };
        }
    }
}

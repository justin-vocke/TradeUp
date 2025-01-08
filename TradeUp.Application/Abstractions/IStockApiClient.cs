using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.DTO.AlphaVantage;

namespace TradeUp.Application.Abstractions
{
    
    public interface IStockApiClient
    {
        Task<StockApiResponse> GetStockInfoAsync(string tickerSymbol);
    }
    
}

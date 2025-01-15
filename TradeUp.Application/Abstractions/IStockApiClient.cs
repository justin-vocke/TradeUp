using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.DTO;
using TradeUp.Application.DTO.AlphaVantage;
using TradeUp.Application.DTO.FinnHub;

namespace TradeUp.Application.Abstractions
{
    
    public interface IStockApiClient
    {
        Task<IStockApiResponse> GetStockInfoAsync(string tickerSymbol);
    }
    
}

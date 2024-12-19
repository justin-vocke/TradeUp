using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Application.Interfaces
{
    
    public interface IStockApiClient
    {
        Task<object> GetStockPriceAsync(string tickerSymbol);
    }
    
}

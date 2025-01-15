using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions;
using TradeUp.Application.Configuration;
using TradeUp.Application.DTO;
using TradeUp.Application.DTO.AlphaVantage;
using TradeUp.Application.DTO.FinnHub;

namespace TradeUp.Infrastructure.Services
{
    public class FinnHubApiClient : IStockApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly FinnHubConfiguration _finnHubConfiguration;

        public FinnHubApiClient(HttpClient httpClient, IOptions<FinnHubConfiguration> finnHubConfiguration)
        {
            _httpClient = httpClient;
            _finnHubConfiguration = finnHubConfiguration.Value;
            _httpClient.BaseAddress = new Uri(_finnHubConfiguration.BaseUrl);
        }
        public async Task<IStockApiResponse> GetStockInfoAsync(string tickerSymbol)
        {
            try
            {
                // Add the symbol as the query parameter
                var response = await _httpClient.GetAsync($"quote?symbol={tickerSymbol}&token={_finnHubConfiguration.ApiKey}");
                var address = _httpClient.BaseAddress + $"quote?symbol={tickerSymbol}&token={_finnHubConfiguration.ApiKey}";

                response.EnsureSuccessStatusCode();
                Console.WriteLine(_httpClient.BaseAddress + $"quote?symbol={tickerSymbol}&token={_finnHubConfiguration.ApiKey}");
                var content = await response.Content.ReadAsStringAsync();
                var stockData = JsonSerializer.Deserialize<FinnHubStockApiResponse>(content);

                // Extract the stock price (adjust the path based on the API's JSON response structure)
                // priceString = stockData?.GlobalQuote?.Price;
                //return decimal.TryParse(priceString, out var price) ? price : null;
                return stockData;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
    }
}

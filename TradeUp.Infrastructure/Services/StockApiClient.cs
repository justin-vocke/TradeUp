using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradeUp.Application.Configuration;
using TradeUp.Application.DTO.AlphaVantage;
using TradeUp.Application.Interfaces;

namespace TradeUp.Infrastructure.Services
{
    public class StockApiClient : IStockApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AlphaVantageConfiguration _alphaVantageConfiguration;

        public StockApiClient(HttpClient httpClient, IOptions<AlphaVantageConfiguration> alphaVantageConfiguration)
        {
            _httpClient = httpClient;
            _alphaVantageConfiguration = alphaVantageConfiguration.Value;
            _httpClient.BaseAddress = new Uri(_alphaVantageConfiguration.BaseUrl);
        }
        public async Task<StockApiResponse> GetStockInfoAsync(string tickerSymbol)
        {
            try
            {
                // Add the symbol as the query parameter
                var response = await _httpClient.GetAsync($"?function=GLOBAL_QUOTE&symbol={tickerSymbol}&apikey={_alphaVantageConfiguration.ApiKey}");
                var address = _httpClient.BaseAddress + $"?function=GLOBAL_QUOTE&symbol={tickerSymbol}&apikey={_alphaVantageConfiguration.ApiKey}";

                response.EnsureSuccessStatusCode();
                Console.WriteLine(_httpClient.BaseAddress + $"?function=GLOBAL_QUOTE&symbol={tickerSymbol}&apikey={_alphaVantageConfiguration.ApiKey}");
                var content = await response.Content.ReadAsStringAsync();
                var stockData = JsonSerializer.Deserialize<StockApiResponse>(content);

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

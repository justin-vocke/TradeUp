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

namespace TradeUp.Infrastructure.Services
{
    public class AlphaVantageApiClient : IStockApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AlphaVantageConfiguration _alphaVantageConfiguration;

        public AlphaVantageApiClient(HttpClient httpClient, IOptions<AlphaVantageConfiguration> alphaVantageConfiguration)
        {
            _httpClient = httpClient;
            _alphaVantageConfiguration = alphaVantageConfiguration.Value;
            _httpClient.BaseAddress = new Uri(_alphaVantageConfiguration.BaseUrl);
        }
        public async Task<IStockApiResponse> GetStockInfoAsync(string tickerSymbol)
        {
            try
            {
                // Add the symbol as the query parameter
                var response = await _httpClient.GetAsync($"?function=GLOBAL_QUOTE&symbol={tickerSymbol}&apikey={_alphaVantageConfiguration.ApiKey}");
                var address = _httpClient.BaseAddress + $"?function=GLOBAL_QUOTE&symbol={tickerSymbol}&apikey={_alphaVantageConfiguration.ApiKey}";

                response.EnsureSuccessStatusCode();
                Console.WriteLine(_httpClient.BaseAddress + $"?function=GLOBAL_QUOTE&symbol={tickerSymbol}&apikey={_alphaVantageConfiguration.ApiKey}");
                var content = await response.Content.ReadAsStringAsync();
                var stockData = JsonSerializer.Deserialize<AlphaVantageApiResponse>(content);

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

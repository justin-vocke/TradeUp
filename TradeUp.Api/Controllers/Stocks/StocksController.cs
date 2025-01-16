using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeUp.Application.Abstractions;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Api.Controllers.Stocks
{
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IStockApiClient _stockApiClient;
        public StocksController(IStockService stockService, IStockApiClient stockApiClient)
        {
            _stockService = stockService;
            _stockApiClient = stockApiClient;
        }

        [HttpPost("CheckThresholds")]
        public async Task<IActionResult> CheckForThresholdAsync()
        {
            await _stockService.CheckThresholdsAsync();
            return Ok();
        }

        [HttpGet("GetStockInfo")]
        public async Task<IActionResult> GetStockInfo([FromQuery] string ticker)
        {
            var result = await _stockApiClient.GetStockInfoAsync(ticker);
            
            return Ok(result);
        }
    }
}

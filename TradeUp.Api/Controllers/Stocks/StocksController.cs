using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Api.Controllers.Stocks
{
    [Route("api/stocks")]
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

        [HttpPost]
        public IActionResult CheckForThreshold([FromBody] Subscription subscription)
        {
            _stockService.SendStockThresholdNotification(subscription);
            return Ok();
        }

        [HttpPost("GetStockPrice")]
        public async Task<IActionResult> GetStockPriceAsync([FromBody] string ticker)
        {
            var result = await _stockApiClient.GetStockPriceAsync(ticker);
            return Ok(result);
        }
    }
}

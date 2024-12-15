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
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public IActionResult CheckForThreshold([FromBody] Subscription subscription)
        {
            _stockService.SendStockThresholdNotification(subscription);
            return Ok();
        }
    }
}

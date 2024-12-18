using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeUp.Application.Commands.Subscriptions;

namespace TradeUp.Api.Controllers.Subscriptions
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionsConroller : ControllerBase
    {
        private readonly ISender _sender;

        public SubscriptionsConroller(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(
            CreateSubscriptionRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateSubscriptionCommand
            (
                request.Id,
                request.Email,
                request.Threshold,
                request.Ticker,
                request.Position
            );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}

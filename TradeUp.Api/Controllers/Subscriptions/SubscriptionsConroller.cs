using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeUp.Application.Commands.Subscriptions;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Api.Controllers.Subscriptions
{
    [Authorize]
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionsConroller : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionsConroller(ISender sender, ISubscriptionRepository subscriptionRepository)
        {
            _sender = sender;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("GetSubscription")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            var sub = await _subscriptionRepository.GetByIdAsync(id);
            return Ok(sub);
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

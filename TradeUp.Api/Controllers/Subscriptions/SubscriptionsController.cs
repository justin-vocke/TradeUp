using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeUp.Application.Abstractions.Authentication;
using TradeUp.Application.Commands.Subscriptions;
using TradeUp.Application.Queries.Subscriptions.GetSubscriptions;
using TradeUp.Application.Queries.Subscriptions.GetSubscriptionsByUserId;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Api.Controllers.Subscriptions
{
    [Authorize]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/subscriptions")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserContext _userContext;

        public SubscriptionsController(ISender sender, ISubscriptionRepository subscriptionRepository, IUserContext userContext)
        {
            _sender = sender;
            _subscriptionRepository = subscriptionRepository;
            _userContext = userContext;
        }

        [HttpGet("GetSubscription")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            var sub = await _subscriptionRepository.GetByIdAsync(id);
            return Ok(sub);
        }

        [HttpGet("GetSubscriptionsForUser")]
        public async Task<IActionResult> GetSubscriptions(CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId.ToString();
            if(userId is  null) {
                return BadRequest();
            }

            var request = new GetSubscriptionsByUserIdQuery(userId);
            var result = await _sender.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(
            CreateSubscriptionRequest request,
            CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var command = new CreateSubscriptionCommand
            (
                userId,
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

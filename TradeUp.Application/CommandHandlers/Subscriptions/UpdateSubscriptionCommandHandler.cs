using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Commands.Subscriptions;
using TradeUp.Application.Exceptions;
using TradeUp.Application.Abstractions;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Interfaces.Repositories;
using TradeUp.Application.Abstractions.Messaging;

namespace TradeUp.Application.CommandHandlers.Subscriptions
{
    internal class UpdateSubscriptionCommandHandler : ICommandHandler<UpdateSubscriptionCommand, UpdateSubscriptionResponse>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<UpdateSubscriptionResponse>> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetByIdAsync(request.Id);

                subscription.Email = request.Email;
                subscription.Threshold = request.Threshold;
                subscription.TickerSymbol = request.Ticker;
                subscription.Position = request.Position;

                _subscriptionRepository.Update(subscription);
                await _unitOfWork.SaveChangesAsync();

                var newSubscriptionResponse = new UpdateSubscriptionResponse
                {
                    Threshold = request.Threshold,
                    Position = request.Position,
                    TickerSymbol = request.Ticker,
                    Email = request.Email,
                    Id = subscription.Id
                };
                return newSubscriptionResponse;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<UpdateSubscriptionResponse>(new Error("Subscription.Overlap", "Error processing new stock subsription"));
            }
        }
    }
}

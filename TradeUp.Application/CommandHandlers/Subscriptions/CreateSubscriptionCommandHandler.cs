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
    internal class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand, Guid>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subscription = Subscription.Create(request.UserId, request.Email, request.Ticker, request.Threshold,
                    request.Position);

                _subscriptionRepository.Add(subscription);
                await _unitOfWork.SaveChangesAsync();

                return subscription.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(new Error("Subscription.Overlap", "Error processing new stock subsription"));
            }
        }
    }
}

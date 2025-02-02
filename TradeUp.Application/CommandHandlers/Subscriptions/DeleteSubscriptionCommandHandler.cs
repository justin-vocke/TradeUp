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
    internal class DeleteSubscriptionCommandHandler : ICommandHandler<DeleteSubscriptionCommand, bool>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<bool>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetByIdAsync(request.Id);

                _subscriptionRepository.Delete(subscription);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success(true);
                
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<bool>(new Error("Subscription.Overlap", "Error processing new stock subsription"));
            }
        }
    }
}

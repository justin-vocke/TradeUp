using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Domain.Core.Bus;
using TradeUp.Application.Commands;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Events;

namespace TradeUp.Application.CommandHandlers
{
    public class PriceThresholdNotificationCommandHandler : IRequestHandler<CreatePriceThresholdNotificationCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public PriceThresholdNotificationCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public Task<bool> Handle(CreatePriceThresholdNotificationCommand request, CancellationToken cancellationToken)
        {
            //TODO: Publish to rabbitmq
            var stockSubscription = new Subscription
            {
                UserId = request.UserId,
                TickerSymbol = request.TickerSymbol,
                Email = request.Email,
                Threshold = request.Threshold,

            };
            _eventBus.Publish(new PriceThresholdReachedEvent(stockSubscription));
            return Task.FromResult(true);
        }
    }
}

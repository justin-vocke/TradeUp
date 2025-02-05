using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Messaging;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Application.Commands.Subscriptions
{
    public sealed record UpdateSubscriptionCommand(Guid UserId, Guid Id, string Email,
      decimal Threshold, string Ticker, ThresholdPosition Position) : ICommand<UpdateSubscriptionResponse>;

}

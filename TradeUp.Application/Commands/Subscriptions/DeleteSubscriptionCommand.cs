using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Messaging;

namespace TradeUp.Application.Commands.Subscriptions
{
    public sealed record DeleteSubscriptionCommand(Guid Id) : ICommand<bool>;

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Application.Commands.Subscriptions
{
    public sealed record CreateSubscriptionCommand(string UserId, string Email,
        decimal Threshold, string Ticker, ThresholdPosition Position) : ICommand<string>;
    
    
}

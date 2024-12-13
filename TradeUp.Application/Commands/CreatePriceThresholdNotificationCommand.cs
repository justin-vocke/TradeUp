using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands
{
    public class CreatePriceThresholdNotificationCommand : PriceThresholdNotificationCommand
    {
        public CreatePriceThresholdNotificationCommand(Guid userId, string email, string tickerSymbol, decimal threshold)
        {
            UserId = userId;
            Email = email;
            TickerSymbol = tickerSymbol;
            Threshold = threshold;
        }
    }
}

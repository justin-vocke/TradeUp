using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands.Subscriptions
{
    public class CreateSubscriptionCommand : Command
    {
        public Guid UserId { get; set; }
        public decimal Threshold { get; set; }
        public string Ticker { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands
{
    public class CreatePriceThresholdNotificationCommand : Command
    {
        public Guid UserId { get; set; }

    }
}

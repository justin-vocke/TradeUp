using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Abstractions;

namespace TradeUp.Domain.Core.Entities
{
    public class Subscription : Entity
    {
       public Guid UserId { get; set; }
       public string Email { get; set; }
        public string TickerSymbol { get; set; }
        public decimal Threshold { get; set; }

    }
}

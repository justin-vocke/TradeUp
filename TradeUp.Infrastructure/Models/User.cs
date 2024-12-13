using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Infrastructure.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}

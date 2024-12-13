using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Infrastructure.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public decimal Threshold { get; set; }
        public string Ticker { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}

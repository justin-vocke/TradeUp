using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands.Users
{
    public class CreateUserCommand : Command
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}

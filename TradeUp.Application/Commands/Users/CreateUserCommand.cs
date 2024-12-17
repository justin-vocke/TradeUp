using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands.Users
{
    public sealed record CreateUserCommand(string Email) : ICommand<Guid>;
    
}

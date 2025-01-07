using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Messaging;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands.Users
{
    public sealed record CreateUserCommand(string FirstName, string LastName, string Email) : ICommand<Guid>;
    
}

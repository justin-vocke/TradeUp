using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Interfaces;

namespace TradeUp.Application.Commands.Users
{
    public sealed record RegisterUserCommand(
        string Email,
        string FirstName,
        string LastName,
        string Password
        ) : ICommand<Guid>;
   
}

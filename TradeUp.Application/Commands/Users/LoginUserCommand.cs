using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Interfaces;

namespace TradeUp.Application.Commands.Users
{
    public sealed record LoginUserCommand(string Email, string Password)
        : ICommand<AccessTokenResponse>;
}

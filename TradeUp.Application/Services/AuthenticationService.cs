using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

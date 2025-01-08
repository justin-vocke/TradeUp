using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Application.Abstractions
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(
            User user,
            string password,
            CancellationToken cancellationToken = default);
    }
}

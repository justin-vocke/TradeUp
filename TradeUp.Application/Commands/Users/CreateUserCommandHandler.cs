using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Commands.Users
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        public Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

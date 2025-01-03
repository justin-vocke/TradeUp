using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Commands.Users;
using TradeUp.Application.Exceptions;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Application.CommandHandlers.Users
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = User.Create(request.Email);
                _userRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();

                return user.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<string>(new Error("User.Overlap", "Error processing new user"));
            }
            
        }
    }
}

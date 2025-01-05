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
    public class RegisterCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(
            IUnitOfWork unitOfWork, 
            IUserRepository userRepository, 
            IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = User.Create(request.Email, request.FirstName, request.LastName);

                var identityId = await _authenticationService.RegisterAsync(
                    user,
                    request.Password,
                    cancellationToken);

                user.SetIdentityId(identityId);

                _userRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();

                return user.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(new Error("User.Overlap", "Error processing new user"));
            }
            
        }
    }
}

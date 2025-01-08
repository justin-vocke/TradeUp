using TradeUp.Application.Commands.Users;
using TradeUp.Application.Exceptions;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities.Users;
using TradeUp.Domain.Core.Interfaces.Repositories;
using TradeUp.Application.Abstractions.Messaging;

namespace TradeUp.Application.CommandHandlers.Users
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = User.Create(request.Email, request.FirstName, request.LastName);
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

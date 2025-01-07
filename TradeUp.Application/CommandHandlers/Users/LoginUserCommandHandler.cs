using TradeUp.Application.Commands.Users;
using TradeUp.Application.Abstractions;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities.Users;
using TradeUp.Application.Abstractions.Messaging;

namespace TradeUp.Application.CommandHandlers.Users
{
    internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
    {
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _jwtService.GetAccessTokenAsync(
                request.Email,
                request.Password,
                cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            return new AccessTokenResponse(result.Value);
        }
    }
}

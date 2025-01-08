using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using Tradeup.Application.Users.GetLoggedInUser;
using TradeUp.Application.Commands.Users;
using TradeUp.Application.Users.GetLoggedInUser;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities;
using TradeUp.Infrastructure.Authorization;

namespace TradeUp.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(
                request.Email,
                request.Password);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("me")]
        [HasPermission(Permissions.UsersRead)]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            Result<UserResponse> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = new CreateUserCommand(request.FirstName, request.LastName,request.Email);

            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}



using Bookify.Application.Abstractions.Messaging;
using TradeUp.Application.Users.GetLoggedInUser;

namespace Tradeup.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;

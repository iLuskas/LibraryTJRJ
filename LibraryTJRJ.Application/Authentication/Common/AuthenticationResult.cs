using LibraryTJRJ.Domain.User;

namespace LibraryTJRJ.Application.Authentication.Common;
public record AuthenticationResult
(
    User User,
    string Token
);

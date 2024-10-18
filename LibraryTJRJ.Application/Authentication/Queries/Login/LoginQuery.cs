using LibraryTJRJ.Application.Authentication.Common;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IQuery<AuthenticationResult>;

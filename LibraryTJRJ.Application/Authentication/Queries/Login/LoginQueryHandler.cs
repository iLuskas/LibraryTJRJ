using ErrorOr;
using LibraryTJRJ.Application.Authentication.Common;
using LibraryTJRJ.Application.Common.Interfaces.Authentication;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.User;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Application.Common.Interfaces.Services;

namespace LibraryTJRJ.Application.Authentication.Queries.Login;

public class LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator,
                               IUserRepository userRepository,
                               IPasswordHasher passwordHasher)
    : IQueryHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (await _userRepository.GetUserByEmailAsync(command.Email) is not User user)
            return Errors.Authentication.InvalidCredentials;

        var hashedPassword = _passwordHasher.Hash(command.Password);

        if (user.Password != hashedPassword)
            return new[] { Errors.Authentication.InvalidCredentials };

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}

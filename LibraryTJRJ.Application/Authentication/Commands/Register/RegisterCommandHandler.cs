using ErrorOr;
using LibraryTJRJ.Application.Authentication.Common;
using LibraryTJRJ.Application.Common.Interfaces.Authentication;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Application.Common.Interfaces.Persistence;
using LibraryTJRJ.Domain.User;
using LibraryTJRJ.Domain.Common.Errors;

namespace LibraryTJRJ.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) != null)
            return Errors.User.DuplicateEmail;

        var user = User.Create(        
            email: command.Email,
            firstName: command.FirstName,
            lastName: command.LastName,
            password: command.Password
        );

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}

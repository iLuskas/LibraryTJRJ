using ErrorOr;
using LibraryTJRJ.Application.Authentication.Common;
using LibraryTJRJ.Application.Common.Interfaces.Authentication;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.User;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;
using LibraryTJRJ.Application.Common.Interfaces.Services;

namespace LibraryTJRJ.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
                                    IUserRepository userRepository,
                                    IUnitOfWork unitOfWork,
                                    IPasswordHasher passwordHasher) : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (await _userRepository.GetUserByEmailAsync(command.Email) != null)
            return Errors.User.DuplicateEmail;

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = User.Create(
            email: command.Email,
            firstName: command.FirstName,
            lastName: command.LastName,
            password: hashedPassword
        );

        await _userRepository.AddAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(
            user,
            token);
    }
}

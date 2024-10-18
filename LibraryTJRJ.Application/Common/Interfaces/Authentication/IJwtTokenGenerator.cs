using LibraryTJRJ.Domain.User;

namespace LibraryTJRJ.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}

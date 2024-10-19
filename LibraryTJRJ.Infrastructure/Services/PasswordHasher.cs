using LibraryTJRJ.Application.Common.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace LibraryTJRJ.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}

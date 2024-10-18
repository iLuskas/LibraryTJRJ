using LibraryTJRJ.Application.Common.Interfaces.Services;

namespace LibraryTJRJ.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

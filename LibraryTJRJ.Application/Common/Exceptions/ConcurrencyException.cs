namespace LibraryTJRJ.Application.Common.Exceptions;

public class ConcurrencyException(string message, Exception innerException) : Exception(message, innerException)
{
}

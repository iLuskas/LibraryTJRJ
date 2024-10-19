using ErrorOr;

namespace LibraryTJRJ.Domain.Common.Errors;

public static partial class Errors
{
    public static class Book
    {
        public static Error NotFound => Error.NotFound(
            code: "Book.NotFound",
            description: "Book not found"
        );
    }
}

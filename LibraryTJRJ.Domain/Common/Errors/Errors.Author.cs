using ErrorOr;

namespace LibraryTJRJ.Domain.Common.Errors;

public static partial class Errors
{
    public static class Author
    {
        public static Error NotFound => Error.NotFound(
            code: "Author.NotFound",
            description: "Author not found"
        );
    }
}

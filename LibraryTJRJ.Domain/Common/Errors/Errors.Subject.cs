using ErrorOr;

namespace LibraryTJRJ.Domain.Common.Errors;

public static partial class Errors
{
    public static class Subject
    {
        public static Error NotFound => Error.NotFound(
            code: "Subject.NotFound",
            description: "Subject not found"
        );
    }
}

using LibraryTJRJ.Contracts.Common;

namespace LibraryTJRJ.Contracts.Books;

public sealed record BookParameters : RequestParameters
{
    public BookParameters()
    {
        SortColumn = "Id";
        SortOrder = "asc";
    }
}

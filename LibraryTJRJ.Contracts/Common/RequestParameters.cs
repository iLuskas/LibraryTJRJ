namespace LibraryTJRJ.Contracts.Common;

public abstract record RequestParameters
{
    const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
    public string? SerachTerm { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
}

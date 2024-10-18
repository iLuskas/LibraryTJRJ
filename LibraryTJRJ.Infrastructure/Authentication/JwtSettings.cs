namespace LibraryTJRJ.Infrastructure.Authentication;

public  class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Secret { get; init; } = null!;
    public int ExpiryMinute { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}

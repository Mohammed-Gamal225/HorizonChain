// Ignore Spelling: Auth Erp

namespace Horizon.Application.Configuration;
public class ErpAuthOptions
{
    public const string SectionName = "ErpAuth";

    public string TokenUrl { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string Scope { get; set; } = null!;
}

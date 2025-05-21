namespace Horizon.Application.Configuration;
public class ErpAuthOptions
{
    public const string SectionName = "ErpAuth";

    public string TokenUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
}

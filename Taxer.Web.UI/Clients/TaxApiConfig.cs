namespace Taxer.Web.UI.Clients;

public class TaxApiConfig
{
    public Uri Uri { get; set; } = default!;
    public string CalculateTaxEndpoint { get; set; } = default!;
}
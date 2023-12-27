namespace Taxer.Core.Entities;

public class TaxType
{
    public string PostalCode { get; init; } = default!;
    public TaxCalculationType CalculationType { get; init; }
}

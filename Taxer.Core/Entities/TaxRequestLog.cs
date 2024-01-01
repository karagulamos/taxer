namespace Taxer.Core.Entities;

public record TaxRequestLog(string PostalCode, TaxCalculationType TaxType, decimal GrossIncome, decimal CalculatedTax, DateTime CreatedAt)
{
    public Guid Id { get; init; }

    public decimal NetIncome => GrossIncome - CalculatedTax;
}
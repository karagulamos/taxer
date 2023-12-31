namespace Taxer.Core.Entities;

public record TaxRequestLog(string PostalCode, TaxCalculationType TaxType, decimal AnnualIncome, decimal CalculatedTax, DateTime CreatedAt)
{
    public Guid Id { get; init; }

    public decimal NetIncome => AnnualIncome - CalculatedTax;
}
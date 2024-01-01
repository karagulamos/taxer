
namespace Taxer.Core.Services.DTOs;

public class CalculateTaxResult
{
    public decimal GrossIncome { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal NetIncome { get; init; }
    public string TaxType { get; init; } = string.Empty;
}

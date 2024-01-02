namespace Taxer.Web.UI.Dtos;

public class CalculateTaxResponseDto
{
    public decimal GrossIncome { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal NetIncome { get; init; }
    public string TaxType { get; init; } = string.Empty;

    public Dictionary<string, string> Errors { get; init; } = [];

    public void AddError(string name, string value) => Errors[name] = value;
}

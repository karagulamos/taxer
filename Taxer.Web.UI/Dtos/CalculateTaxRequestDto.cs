using Taxer.Web.UI.Models;

namespace Taxer.Web.UI.Dtos;

public record CalculateTaxRequestDto(decimal GrossIncome, string PostalCode)
{
    public static implicit operator CalculateTaxRequestDto(TaxViewModel model) => new(model.GrossIncome, model.PostalCode);
}

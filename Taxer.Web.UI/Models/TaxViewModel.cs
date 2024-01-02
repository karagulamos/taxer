using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Taxer.Web.UI.Dtos;
using Taxer.Web.UI.Extensions;

namespace Taxer.Web.UI.Models;

[Bind(nameof(GrossIncome), nameof(PostalCode))]
public record TaxViewModel(
    [property: DisplayName("Gross Income")]
    [Display(Name = "Gross Income")]
    [Required(ErrorMessage = "{0} is required")]
    decimal GrossIncome,

    [property: DisplayName("Postal Code")]
    [Display(Name = "Postal Code")]
    [Required(ErrorMessage = "{0} is required")]
    string PostalCode
)
{
    public bool HasResult { get; init; }
    public decimal Tax { get; init; }
    public decimal NetIncome { get; init; }
    public string TaxType { get; init; } = string.Empty;

    public TaxViewModel WithResult(CalculateTaxResponseDto result) => this with
    {
        HasResult = result.Errors.Count == 0,
        Tax = result.TaxAmount,
        NetIncome = result.NetIncome,
        TaxType = result.TaxType.ToSeparatedWords()
    };
}

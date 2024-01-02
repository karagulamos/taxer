using System.ComponentModel.DataAnnotations;
using Taxer.Core.Entities;

namespace Taxer.Core.Services.DTOs;

public class CalculateTaxRequest
{
    [Display(Name = "Gross Income")]
    [Required(ErrorMessage = "{0} is required.")]
    public decimal GrossIncome { get; set; }

    [Display(Name = "Postal Code")]
    [Required(ErrorMessage = "{0} is required.")]
    public string PostalCode { get; set; } = string.Empty;

    public TaxRequestLog ToEntity(decimal tax, TaxCalculationType taxType) => new(PostalCode, taxType, GrossIncome, tax, DateTime.UtcNow);
}
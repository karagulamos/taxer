using System.ComponentModel.DataAnnotations;
using Taxer.Core.Entities;

namespace Taxer.Core.Services.DTOs;

public class CalculateTaxRequest
{
    [Required(ErrorMessage = "Income is required.")]
    public decimal Income { get; set; }

    [Required(ErrorMessage = "Postal code is required.")]
    public string PostalCode { get; set; } = string.Empty;

    public TaxRequestLog ToEntity(decimal tax, TaxCalculationType taxType) => new(PostalCode, taxType, Income, tax, DateTime.UtcNow);
}
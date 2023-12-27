using System.ComponentModel.DataAnnotations;

namespace Taxer.Core.Services.DTOs;

public class CalculateTaxRequest
{
    [Required(ErrorMessage = "Income is required.")]
    public decimal Income { get; set; }

    [Required(ErrorMessage = "Postal code is required.")]
    public string PostalCode { get; set; } = string.Empty;
}

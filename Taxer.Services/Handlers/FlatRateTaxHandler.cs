using Taxer.Core;
using Taxer.Core.Services.Handlers;

namespace Taxer.Services.Handlers;

public class FlatRateTaxCalculatorHandler : TaxCalculatorHandler
{
    // Ideally, this would be stored in a database or configuration file and retrieved at runtime.
    private const decimal TaxRate = 0.175m;

    protected override Task<decimal> CalculateTaxAsync(decimal income, TaxCalculationType calculationType)
    {
        var taxAmount = income * TaxRate;
        return Task.FromResult(taxAmount);
    }

    protected override bool CanHandle(TaxCalculationType calculationType) => calculationType == TaxCalculationType.FlatRate;
}

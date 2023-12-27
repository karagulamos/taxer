using Taxer.Core;
using Taxer.Core.Services.Handlers;

namespace Taxer.Services.Handlers;

public class FlatValueTaxCalculatorHandler : TaxCalculatorHandler
{
    // Ideally, this would be stored in a database or configuration file and retrieved at runtime.
    private const decimal FlatValueTaxThreshold = 200000;
    private const decimal FlatValueTaxAmount = 10000;
    private const decimal TaxRateBelowThreshold = 0.05m;

    protected override Task<decimal> CalculateTaxAsync(decimal income, TaxCalculationType calculationType)
    {
        if (income >= FlatValueTaxThreshold)
            return Task.FromResult(FlatValueTaxAmount);

        var taxAmount = income * TaxRateBelowThreshold;
        return Task.FromResult(taxAmount);
    }

    protected override bool CanHandle(TaxCalculationType calculationType) => calculationType == TaxCalculationType.FlatValue;
}
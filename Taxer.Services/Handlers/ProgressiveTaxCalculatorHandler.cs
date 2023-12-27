using Taxer.Core;
using Taxer.Core.Services.Handlers;

namespace Taxer.Services.Handlers;

public class ProgressiveTaxCalculatorHandler : TaxCalculatorHandler
{
    // Ideally, this would be stored in a database or configuration file and retrieved at runtime.
    private static readonly (decimal Rate, decimal Max)[] TaxBrackets =
    [
        (0.1m, 8350), (0.15m, 33950), (0.25m, 82250), (0.28m, 171550),
        (0.33m, 372950), (0.35m, decimal.MaxValue)
    ];

    protected override Task<decimal> CalculateTaxAsync(decimal income, TaxCalculationType calculationType)
    {
        var tax = 0m;

        foreach (var bracket in TaxBrackets)
        {
            if (income <= 0)
                break;

            var taxableAmount = Math.Min(income, bracket.Max);

            tax += taxableAmount * bracket.Rate;

            income -= taxableAmount;
        }

        return Task.FromResult(tax);
    }

    protected override bool CanHandle(TaxCalculationType calculationType) => calculationType == TaxCalculationType.Progressive;
}
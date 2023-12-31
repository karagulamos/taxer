using Taxer.Core;
using Taxer.Core.Services;

namespace Taxer.Services.Handlers;

public class ProgressiveTaxCalculatorHandler : BaseTaxCalculatorHandler, ITaxCalculatorHandler
{
    // Ideally, this would be stored in a database or configuration file and retrieved at runtime.
    private static readonly (decimal Rate, decimal Max)[] TaxBrackets =
    [
        (0.1m, 8350), (0.15m, 33950), (0.25m, 82250), (0.28m, 171550),
        (0.33m, 372950), (0.35m, decimal.MaxValue)
    ];

    protected override Task<decimal> CalculateTaxAsync(decimal income, TaxCalculationType calculationType)
    {
        var totalTax = 0m;

        foreach (var bracket in TaxBrackets)
        {
            // If the income is less than or equal to zero, we're done.
            if (income <= 0)
                break;

            // For the current bracket, calculate the taxable amount.
            var taxableAmount = Math.Min(income, bracket.Max);

            // Compute the tax for the current bracket and add it to the total tax.
            totalTax += taxableAmount * bracket.Rate;

            // Subtract the taxable amount from the income.
            income -= taxableAmount;
        }

        return Task.FromResult(totalTax);
    }

    protected override bool CanHandle(TaxCalculationType calculationType) => calculationType == TaxCalculationType.Progressive;
}
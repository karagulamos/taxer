using Taxer.Core;
using Taxer.Core.Services;

namespace Taxer.Services.Handlers;

public abstract class BaseTaxCalculatorHandler
{
    private ITaxCalculatorHandler? _next;

    public void SetNext(ITaxCalculatorHandler next) => _next = next;

    public Task<decimal> HandleAsync(decimal income, TaxCalculationType calculationType)
    {
        if (CanHandle(calculationType))
            return CalculateTaxAsync(income, calculationType);

        if (_next is null)
            throw new InvalidOperationException($"No handler found for tax calculation type {calculationType}");

        return _next.HandleAsync(income, calculationType);
    }

    protected abstract Task<decimal> CalculateTaxAsync(decimal income, TaxCalculationType calculationType);
    protected abstract bool CanHandle(TaxCalculationType calculationType);
}

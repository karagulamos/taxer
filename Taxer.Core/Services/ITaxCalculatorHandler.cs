namespace Taxer.Core.Services;

public interface ITaxCalculatorHandler
{
    Task<decimal> HandleAsync(decimal income, TaxCalculationType calculationType);
}
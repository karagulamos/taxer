namespace Taxer.Core.Services.Handlers;

public interface ITaxCalculatorHandler
{
    Task<decimal> HandleAsync(decimal income, TaxCalculationType calculationType);
}
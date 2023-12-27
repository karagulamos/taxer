using Taxer.Core;
using Taxer.Services.Handlers;

namespace Taxer.UnitTests.System.Services;

public class FlatValueTaxCalculatorHandlerTests
{
    [Test]
    public async Task Handle_Applies_FixedAmount_When_Income_Is_Above_Threshold()
    {
        // Arrange
        var income = 200000;
        var handler = new FlatValueTaxCalculatorHandler();

        // Act
        var taxAmount = await handler.HandleAsync(income, TaxCalculationType.FlatValue);

        // Assert
        Assert.That(taxAmount, Is.EqualTo(10000));
    }

    [Test]
    public async Task Handle_Applies_PercentTaxRate_When_Income_Is_Below_Threshold()
    {
        // Arrange
        var income = 1000;
        var handler = new FlatValueTaxCalculatorHandler();

        // Act
        var taxAmount = await handler.HandleAsync(income, TaxCalculationType.FlatValue);

        // Assert
        Assert.That(taxAmount, Is.EqualTo(50));
    }
}

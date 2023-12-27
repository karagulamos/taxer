using Taxer.Core;
using Taxer.Services.Handlers;

namespace Taxer.UnitTests.System.Services;

public class ProgressiveTaxCalculatorHandlerTests
{
    [TestCase(0, 0)]
    [TestCase(5000, 500)]
    [TestCase(20000, 2582.5)]
    [TestCase(8350, 835)]
    [TestCase(50000, 7852.5)]
    [TestCase(172950, 40042)]
    [TestCase(1000000, 313430)]
    public async Task Handle_Returns_ProgressiveTaxAmount_Given_Incomes(decimal income, decimal expectedTaxAmount)
    {
        // Arrange
        var handler = new ProgressiveTaxCalculatorHandler();

        // Act
        var taxAmount = await handler.HandleAsync(income, TaxCalculationType.Progressive);

        // Assert
        Assert.That(taxAmount, Is.EqualTo(expectedTaxAmount));
    }
}
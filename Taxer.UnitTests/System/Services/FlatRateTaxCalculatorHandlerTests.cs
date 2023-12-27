using Taxer.Core;
using Taxer.Services.Handlers;

namespace Taxer.UnitTests.System.Services;

public class FlatRateTaxCalculatorHandlerTests
{
    [Test]
    public async Task HandleAsync_ComputesTax()
    {
        // Arrange
        var income = 1000;
        var handler = new FlatRateTaxCalculatorHandler();

        // Act
        var taxAmount = await handler.HandleAsync(income, TaxCalculationType.FlatRate);

        // Assert
        Assert.That(taxAmount, Is.EqualTo(175));
    }
}

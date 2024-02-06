using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxer.Core.Common;
using Taxer.Core.Errors;
using Taxer.Core.Services;
using Taxer.Core.Services.DTOs;
using Taxer.Web.API;

namespace Taxer.UnitTests.System.Controllers;

public class TaxControllerTests
{
    private readonly Mock<ITaxService> _taxServiceMock;

    public TaxControllerTests() => _taxServiceMock = new();

    [SetUp]
    public void Setup()
    {
        _taxServiceMock.Setup(x => x.CalculateTaxAsync(It.IsAny<CalculateTaxRequest>()))
                       .ReturnsAsync(new CalculateTaxResult { });
    }

    [Test]
    public async Task CaclulateTax_Returns_Ok_Given_ValidRequest()
    {
        // Arrange
        var controller = new TaxController(_taxServiceMock.Object);

        // Act
        var result = await controller.CalculateTaxAsync(new CalculateTaxRequest { GrossIncome = 1000, PostalCode = "7441" });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.TypeOf<CalculateTaxResult>());
        });
    }

    [Test]
    public async Task CalculateTax_Returns_BadRequest_Given_BadRequest()
    {
        // Arrange
        var controller = new TaxController(_taxServiceMock.Object);

        controller.ModelState.AddModelError("Income", "Income is required.");
        controller.ModelState.AddModelError("PostalCode", "Postal code is required.");

        // Act
        var result = await controller.CalculateTaxAsync(new CalculateTaxRequest());

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [TestCaseSource(nameof(InvalidServiceRequestScenarios))]
    public async Task CalculateTax_Returns_BadRequest_Given_InvalidServiceRequests(CalculateTaxRequest request, Error error)
    {
        // Arrange
        _taxServiceMock.Setup(x => x.CalculateTaxAsync(It.IsAny<CalculateTaxRequest>()))
                       .ReturnsAsync(error);

        var controller = new TaxController(_taxServiceMock.Object);

        // Act
        var result = await controller.CalculateTaxAsync(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo(error));
        });
    }

    private static IEnumerable<object> InvalidServiceRequestScenarios()
    {
        yield return new TestCaseData(new CalculateTaxRequest { PostalCode = "7441" }, ServiceErrors.Tax.InvalidIncome);
        yield return new TestCaseData(new CalculateTaxRequest { GrossIncome = 1000 }, ServiceErrors.Tax.InvalidPostalCode);
        yield return new TestCaseData(new CalculateTaxRequest { GrossIncome = 1000, PostalCode = "0000" }, ServiceErrors.Tax.UnsupportedPostalCode);
    }
}

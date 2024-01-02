using Moq;
using Taxer.Core;
using Taxer.Core.Common;
using Taxer.Core.Entities;
using Taxer.Core.Errors;
using Taxer.Core.Persistence.Repositories;
using Taxer.Core.Services.DTOs;
using Taxer.Core.Services.Handlers;
using Taxer.Services;

namespace Taxer.UnitTests.System.Services;

public class TaxServiceTests
{
    private readonly Mock<ITaxTypeRepository> _taxTypeRepository;
    private readonly Mock<ITaxRequestLogRepository> _taxRequestLogRepository;
    private readonly Mock<ITaxCalculatorHandler> _taxCalculatorHandler;

    public TaxServiceTests()
    {
        _taxTypeRepository = new Mock<ITaxTypeRepository>();
        _taxRequestLogRepository = new Mock<ITaxRequestLogRepository>();
        _taxCalculatorHandler = new Mock<ITaxCalculatorHandler>();
    }

    [SetUp]
    public void Setup()
    {
        _taxTypeRepository.Setup(x => x.GetByPostalCodeAsync(It.IsAny<string>()))
                          .ReturnsAsync(new TaxType(default!, TaxCalculationType.FlatRate))
                          .Verifiable();

        _taxCalculatorHandler.Setup(x => x.HandleAsync(It.IsAny<decimal>(), It.IsAny<TaxCalculationType>()))
                             .ReturnsAsync(It.IsAny<decimal>())
                             .Verifiable();
    }

    [Test]
    public async Task CalculateTax_Computes_TaxResult_Given_ValidRequest()
    {
        // Arrange
        var service = new TaxService(_taxTypeRepository.Object, _taxRequestLogRepository.Object, _taxCalculatorHandler.Object);

        // Act
        var result = await service.CalculateTaxAsync(new CalculateTaxRequest { GrossIncome = 1000, PostalCode = "7441" });

        // Assert
        Assert.That(result.IsSuccess, Is.True);
    }

    [TestCaseSource(nameof(InvalidServiceRequestScenarios))]
    public async Task CalculateTax_Returns_Error_Given_InvalidServiceRequests(CalculateTaxRequest request, Error error)
    {
        // Arrange
        var service = new TaxService(_taxTypeRepository.Object, _taxRequestLogRepository.Object, _taxCalculatorHandler.Object);

        // Act
        var result = await service.CalculateTaxAsync(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task CalculateTax_Returns_UnsupportedPostalCode_Given_PostalCodeNotInDatabase()
    {
        // Arrange
        _taxTypeRepository.Setup(x => x.GetByPostalCodeAsync(It.IsAny<string>()))
                          .ReturnsAsync(default(TaxType))
                          .Verifiable();

        var service = new TaxService(_taxTypeRepository.Object, _taxRequestLogRepository.Object, _taxCalculatorHandler.Object);

        // Act
        var result = await service.CalculateTaxAsync(new CalculateTaxRequest { GrossIncome = 1000, PostalCode = "0000" });

        // Assert
        Assert.Multiple(() =>
        {
            _taxTypeRepository.Verify(x => x.GetByPostalCodeAsync(It.IsAny<string>()), Times.Once);

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo(ServiceErrors.Tax.UnsupportedPostalCode));
        });
    }

    [Test]
    public async Task CalculateTax_Applies_TaxCalculatorHandler_Given_ValidRequest()
    {
        // Arrange
        var service = new TaxService(_taxTypeRepository.Object, _taxRequestLogRepository.Object, _taxCalculatorHandler.Object);

        // Act
        var result = await service.CalculateTaxAsync(new CalculateTaxRequest { GrossIncome = 1000, PostalCode = "7441" });

        // Assert
        _taxCalculatorHandler.Verify(x => x.HandleAsync(It.IsAny<decimal>(), It.IsAny<TaxCalculationType>()), Times.Once);
    }

    [Test]
    public async Task CalculateTax_Adds_TaxRequestLog_Given_ValidRequest()
    {
        // Arrange
        var service = new TaxService(_taxTypeRepository.Object, _taxRequestLogRepository.Object, _taxCalculatorHandler.Object);

        // Act
        var result = await service.CalculateTaxAsync(new CalculateTaxRequest { GrossIncome = 1000, PostalCode = "7441" });

        // Assert
        _taxRequestLogRepository.Verify(x => x.AddAsync(It.IsAny<TaxRequestLog>()), Times.Once);
    }

    private static IEnumerable<object> InvalidServiceRequestScenarios()
    {
        yield return new object[] { new CalculateTaxRequest { PostalCode = "7441" }, ServiceErrors.Tax.InvalidIncome };
        yield return new object[] { new CalculateTaxRequest { GrossIncome = 1000 }, ServiceErrors.Tax.InvalidPostalCode };
    }

    [TearDown]
    public void TearDown()
    {
        _taxTypeRepository.Reset();
        _taxRequestLogRepository.Reset();
        _taxCalculatorHandler.Reset();
    }
}

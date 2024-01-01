using Taxer.Core.Common;
using Taxer.Core.Errors;
using Taxer.Core.Persistence.Repositories;
using Taxer.Core.Services;
using Taxer.Core.Services.DTOs;
using Taxer.Core.Services.Handlers;

namespace Taxer.Services;

public class TaxService(
    ITaxTypeRepository taxTypeRepository, ITaxRequestLogRepository taxRequestLogRepository,
    ITaxCalculatorHandler taxCalculatorHandler) : ITaxService
{
    public async Task<Result<CalculateTaxResult>> CalculateTaxAsync(CalculateTaxRequest request)
    {
        if (request.Income <= 0)
            return ServiceErrors.Tax.InvalidIncome;

        if (string.IsNullOrEmpty(request.PostalCode))
            return ServiceErrors.Tax.InvalidPostalCode;

        var taxType = await taxTypeRepository.GetByPostalCodeAsync(request.PostalCode);

        if (taxType is null)
            return ServiceErrors.Tax.UnsupportedPostalCode;

        var taxAmount = await taxCalculatorHandler.HandleAsync(request.Income, taxType.CalculationType);

        var taxRequestLog = request.ToEntity(taxAmount, taxType.CalculationType);

        await taxRequestLogRepository.AddAsync(taxRequestLog);

        return new CalculateTaxResult
        {
            TaxType = taxRequestLog.TaxType.ToString(),
            TaxAmount = taxRequestLog.CalculatedTax,
            NetIncome = taxRequestLog.NetIncome,
            GrossIncome = taxRequestLog.GrossIncome
        };
    }
}
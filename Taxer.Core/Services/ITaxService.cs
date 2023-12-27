using Taxer.Core.Common;
using Taxer.Core.Services.DTOs;

namespace Taxer.Core.Services;

public interface ITaxService
{
    Task<Result<CalculateTaxResult>> CalculateTaxAsync(CalculateTaxRequest request);
}

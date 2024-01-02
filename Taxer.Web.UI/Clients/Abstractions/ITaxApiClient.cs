using Taxer.Web.UI.Dtos;

namespace Taxer.Web.UI.Clients.Abstractions;

public interface ITaxApiClient
{
    Task<CalculateTaxResponseDto> CalculateTax(CalculateTaxRequestDto model, CancellationToken cancellationToken = default);
}

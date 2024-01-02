using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Taxer.Web.UI.Clients.Abstractions;
using Taxer.Web.UI.Dtos;

namespace Taxer.Web.UI.Clients;

public class TaxApiClient(HttpClient httpClient, IOptionsMonitor<TaxApiConfig> config) : ITaxApiClient
{
    public async Task<CalculateTaxResponseDto> CalculateTax(CalculateTaxRequestDto request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(config.CurrentValue.CalculateTaxEndpoint, request, cancellationToken);

        var result = new CalculateTaxResponseDto();

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken);

            // Handles the case where the API returns a model state dictionary due to validation errors
            if (errors!.TryGetValue(nameof(errors), out var errorMap))
            {
                var errorsList = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorMap.ToString()!);

                foreach (var error in errorsList!)
                    result.AddError(error.Key, error.Value.First());

                return result;
            }

            // Handles the case where the API returns a custom error due to a business rule violation
            if (errors!.TryGetValue("message", out var message))
            {
                result.AddError(string.Empty, message.ToString()!);
                return result;
            }
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CalculateTaxResponseDto>(cancellationToken)! ?? result;
    }
}
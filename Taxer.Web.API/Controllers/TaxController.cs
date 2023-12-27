using Microsoft.AspNetCore.Mvc;
using Taxer.Core.Services;
using Taxer.Core.Services.DTOs;

namespace Taxer.Web.API;

[ApiController]
[Route("api/v1/[controller]")]
public class TaxController(ITaxService taxService) : ControllerBase
{
    [HttpPost("calculate", Name = nameof(CalculateTaxAsync))]
    [ProducesResponseType(typeof(CalculateTaxResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalculateTaxAsync([FromBody] CalculateTaxRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await taxService.CalculateTaxAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
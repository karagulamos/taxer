using Microsoft.AspNetCore.Mvc;
using Taxer.Web.UI.Clients.Abstractions;
using Taxer.Web.UI.Extensions;
using Taxer.Web.UI.Models;

namespace Taxer.Web.UI;

[Route("tax")]
public class TaxController(ITaxApiClient client, ILogger<TaxController> logger) : Controller
{
    private const string ErrorCalculatingTax = "An error occurred while calculating tax";

    [HttpGet]
    [Route("calculator")]
    public IActionResult Index() => View();

    [HttpPost]
    [Route("calculator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(TaxViewModel model)
    {
        logger.LogInformation("Tax calculation has been requested");

        try
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await client.CalculateTaxAsync(model);

            ModelState.AddModelErrors(result.Errors);

            return View(model.WithResult(result));
        }
        catch (HttpRequestException httpex)
        {
            logger.LogError(httpex, ErrorCalculatingTax);
            ModelState.AddModelError(string.Empty, ErrorCalculatingTax);
            return View(model);
        }
    }
}

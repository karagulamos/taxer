using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taxer.Web.UI.Models;

namespace Taxer.Web.UI.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        logger.LogInformation("Home page has been requested");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
    }
}

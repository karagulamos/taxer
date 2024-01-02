using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Taxer.Web.UI.Extensions;

public static class ModelExtensions
{
    public static void AddModelErrors(this ModelStateDictionary modelState, IDictionary<string, string> errors)
    {
        foreach (var error in errors)
            modelState.AddModelError(error.Key, error.Value);
    }
}

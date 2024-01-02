using Taxer.Web.UI.Clients;
using Taxer.Web.UI.Clients.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<TaxApiConfig>(builder.Configuration.GetSection(nameof(TaxApiConfig)));

builder.Services.AddHttpClient<ITaxApiClient, TaxApiClient>(client =>
{
    var config = builder.Configuration.GetSection(nameof(TaxApiConfig)).Get<TaxApiConfig>()!;
    client.BaseAddress = config.Uri;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

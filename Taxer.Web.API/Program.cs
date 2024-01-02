using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Taxer.Core.Persistence.Repositories;
using Taxer.Core.Services;
using Taxer.Persistence.EntityFramework;
using Taxer.Persistence.EntityFramework.Repositories;
using Taxer.Services;
using Taxer.Services.Handlers;
using Taxer.Web.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new() { Title = "Taxer API", Version = "v1" });
});

builder.Services.AddTransient<ITaxTypeRepository, TaxTypeRepository>();
builder.Services.AddTransient<ITaxRequestLogRepository, TaxRequestLogRepository>();
builder.Services.AddTransient<ITaxService, TaxService>();
builder.Services.AddTransient<ITaxCalculatorHandler>(x =>
{
    var progressiveTaxHandler = new ProgressiveTaxCalculatorHandler();
    var flatValueTaxHander = new FlatValueTaxCalculatorHandler();
    var flatRateTaxHandler = new FlatRateTaxCalculatorHandler();

    progressiveTaxHandler.SetNext(flatValueTaxHander);
    flatValueTaxHander.SetNext(flatRateTaxHandler);

    return progressiveTaxHandler;
});

builder.Services.AddDbContext<TaxerContext>(async options =>
{
    options.UseInMemoryDatabase("TaxerDb")
           .EnableSensitiveDataLogging()
           .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));

    await using var context = new TaxerContext(options.Options);
    await context.Database.EnsureCreatedAsync();
});

builder.Services.AddSingleton<ExecutionTrackerMiddleware>();
builder.Services.AddSingleton<DefaultExceptionHandlerMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExecutionTrackerMiddleware>();
app.UseMiddleware<DefaultExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Taxer.Core.Persistence.Repositories;
using Taxer.Core.Services;
using Taxer.Core.Services.Handlers;
using Taxer.Persistence.EntityFramework;
using Taxer.Persistence.EntityFramework.Repositories;
using Taxer.Services;
using Taxer.Services.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new() { Title = "Taxer API", Version = "v1" });
});

builder.Services.AddTransient<ITaxTypeRepository, TaxTypeRepository>();
builder.Services.AddTransient<ITaxService, TaxService>();
builder.Services.AddTransient<ITaxCalculatorHandler>(x =>
{
    var flatValueHander = new FlatValueTaxCalculatorHandler();
    var flatRateHandler = new FlatRateTaxCalculatorHandler();

    flatValueHander.SetNext(flatRateHandler);
    flatRateHandler.SetNext(new ProgressiveTaxCalculatorHandler());

    return flatValueHander;
});

builder.Services.AddDbContext<TaxerContext>(async options =>
{
    options.UseInMemoryDatabase("TaxerDb")
           .EnableSensitiveDataLogging()
           .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));

    await using var context = new TaxerContext(options.Options);
    await context.Database.EnsureCreatedAsync();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

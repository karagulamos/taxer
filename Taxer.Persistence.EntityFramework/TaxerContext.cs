using Microsoft.EntityFrameworkCore;
using Taxer.Core;
using Taxer.Core.Entities;

namespace Taxer.Persistence.EntityFramework;

public class TaxerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaxType> TaxTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxType>().HasKey(x => new { x.PostalCode, x.CalculationType });

        modelBuilder.Entity<TaxType>().HasData(
            new TaxType("7441", TaxCalculationType.Progressive),
            new TaxType("A100", TaxCalculationType.FlatValue),
            new TaxType("7000", TaxCalculationType.FlatRate),
            new TaxType("1000", TaxCalculationType.Progressive)
        );
    }
}
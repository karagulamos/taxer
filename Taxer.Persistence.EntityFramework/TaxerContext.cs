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
            new TaxType { PostalCode = "7441", CalculationType = TaxCalculationType.Progressive },
            new TaxType { PostalCode = "A100", CalculationType = TaxCalculationType.FlatValue },
            new TaxType { PostalCode = "7000", CalculationType = TaxCalculationType.FlatRate },
            new TaxType { PostalCode = "1000", CalculationType = TaxCalculationType.Progressive }
        );
    }
}
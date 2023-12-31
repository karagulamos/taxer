using Microsoft.EntityFrameworkCore;
using Taxer.Core;
using Taxer.Core.Entities;

namespace Taxer.Persistence.EntityFramework;

public class TaxerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaxType> TaxTypes { get; set; }
    public DbSet<TaxRequestLog> TaxRequestLogs { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxRequestLog>().HasKey(x => x.Id);
        modelBuilder.Entity<TaxRequestLog>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<TaxType>().HasKey(x => new { x.PostalCode, x.CalculationType });

        modelBuilder.Entity<TaxType>().HasData(
            new TaxType("7441", TaxCalculationType.Progressive),
            new TaxType("A100", TaxCalculationType.FlatValue),
            new TaxType("7000", TaxCalculationType.FlatRate),
            new TaxType("1000", TaxCalculationType.Progressive)
        );
    }
}
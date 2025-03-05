using Microsoft.EntityFrameworkCore;
using TaxCalculator.Domain.Models;

namespace TaxCalculator.Infra.Data
{
    public class TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : DbContext(options)
    {
        public DbSet<TaxBand> TaxBands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxBand>(e =>
            {
                e.ToTable("TaxBands", "TaxCalculator");
                e.HasKey(x => x.LowerLimit);
                e.Property(x => x.LowerLimit).HasColumnType("decimal(18,2)");
                e.Property(x => x.Rate).HasColumnType("decimal(18,2)");

                e.HasData([
                    new TaxBand { LowerLimit = 0, Rate = 0 },
                    new TaxBand { LowerLimit = 5000, Rate = 20 },
                    new TaxBand { LowerLimit = 20_000, Rate = 40 },
                ]);
            });
        }
    }
}
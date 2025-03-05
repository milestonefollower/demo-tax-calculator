using Microsoft.EntityFrameworkCore;
using TaxCalculator.Domain.Models;
using TaxCalculator.Infra.Abstraction;
using TaxCalculator.Infra.Data;

namespace TaxCalculator.Infra.Repositories
{
    public class TaxBandRepository : ITaxBandRepository
    {
        private readonly TaxCalculatorDbContext _context;

        public TaxBandRepository(TaxCalculatorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxBand>> GetAllTaxBandsAsync()
        {
            return await _context.TaxBands.ToListAsync();
        }

        public Task AddTaxBandAsync(TaxBand taxBand)
        {
            _context.TaxBands.Add(taxBand);
            return _context.SaveChangesAsync();
        }
    }
}
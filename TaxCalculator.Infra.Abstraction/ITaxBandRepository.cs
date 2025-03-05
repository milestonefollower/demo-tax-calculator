
using TaxCalculator.Domain.Models;

namespace TaxCalculator.Infra.Abstraction
{
    public interface ITaxBandRepository
    {
        Task<IEnumerable<TaxBand>> GetAllTaxBandsAsync();
        Task AddTaxBandAsync(TaxBand taxBand);
    }
}
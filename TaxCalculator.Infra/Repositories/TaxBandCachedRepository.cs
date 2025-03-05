using System;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.Domain.Models;
using TaxCalculator.Infra.Abstraction;

namespace TaxCalculator.Infra.Repositories;

public class TaxBandCachedRepository : ITaxBandRepository
{
    private readonly TaxBandRepository _repo;
    private readonly IMemoryCache _cache;
    private const string _cacheKey = "TaxBandCache";

    public TaxBandCachedRepository(TaxBandRepository repository, IMemoryCache cache)
    {
        _repo = repository ?? throw new ArgumentNullException(nameof(repository));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    public Task AddTaxBandAsync(TaxBand taxBand) => _repo.AddTaxBandAsync(taxBand);

    public Task<IEnumerable<TaxBand>> GetAllTaxBandsAsync()
    {
        return _cache.GetOrCreateAsync(_cacheKey, x =>
        {
            x.SetAbsoluteExpiration(TimeSpan.FromHours(1));
            return  _repo.GetAllTaxBandsAsync();
        });
    }
}

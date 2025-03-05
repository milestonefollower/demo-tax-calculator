using TaxCalculator.BL.DTOs;
using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.TaxCalculationEngine;
using TaxCalculator.Infra.Abstraction;

namespace TaxCalculator.BL.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly ITaxBandRepository _repository;

        public TaxCalculatorService(ITaxBandRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxResultDTO> CalculateTaxAsync(decimal grossSalary)
        {
            var taxBands = await _repository.GetAllTaxBandsAsync();
            return TaxCalculationEngine.TaxCalculationEngine.CalculateTax(grossSalary, taxBands);
        }
    }
}
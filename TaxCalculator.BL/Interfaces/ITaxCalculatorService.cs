using TaxCalculator.BL.DTOs;

namespace TaxCalculator.BL.Interfaces
{
    public interface ITaxCalculatorService
    {
        Task<TaxResultDTO> CalculateTaxAsync(decimal grossSalary);
    }
}
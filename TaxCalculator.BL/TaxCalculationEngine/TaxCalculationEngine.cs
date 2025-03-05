using System;
using TaxCalculator.BL.DTOs;
using TaxCalculator.Domain.Models;

namespace TaxCalculator.BL.TaxCalculationEngine;

internal static class TaxCalculationEngine
{
    internal static TaxResultDTO CalculateTax(decimal annualGrossSalary, IEnumerable<TaxBand> taxBands)
    {
        var result = new TaxResultDTO
        {
            AnnualGrossSalary = annualGrossSalary
        };

        var fitBands = taxBands.Where(x => x.LowerLimit < annualGrossSalary);

        decimal taxablePart = annualGrossSalary;
        foreach (var taxBand in fitBands.OrderByDescending(x => x.LowerLimit))
        {
            result.AnnualTaxPaid += (taxablePart - taxBand.LowerLimit) * taxBand.Rate * 0.01m;
            taxablePart = taxBand.LowerLimit;
        }
        return result;
    }
}

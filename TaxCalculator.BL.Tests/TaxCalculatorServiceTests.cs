using Moq;
using TaxCalculator.BL.DTOs;
using TaxCalculator.BL.Services;
using TaxCalculator.Domain.Models;
using TaxCalculator.Infra.Abstraction;

namespace TaxCalculator.BL.Tests;

public class TaxCalculatorServiceTests
{
    private readonly TaxCalculatorService _taxCalculatorService;
    private readonly Mock<ITaxBandRepository> _taxBandRepositoryMock = new Mock<ITaxBandRepository>();

    public TaxCalculatorServiceTests()
    {
        _taxCalculatorService = new TaxCalculatorService(_taxBandRepositoryMock.Object);
    }

    public static IEnumerable<object[]> SalaryTaxData =>
        new List<object[]>
        {
            new object[] {
                10_000, new List<TaxBand>
                {
                    new TaxBand { LowerLimit = 0, Rate = 0 },
                    new TaxBand { LowerLimit = 5_000, Rate = 20 },
                    new TaxBand { LowerLimit = 20_000, Rate = 40 }
                },
                new TaxResultDTO { AnnualGrossSalary = 10_000, AnnualTaxPaid = 1_000 }
            },
            new object[] {
                40_000, new List<TaxBand>
                {
                    new TaxBand { LowerLimit = 0, Rate = 0 },
                    new TaxBand { LowerLimit = 5_000, Rate = 20 },
                    new TaxBand { LowerLimit = 20_000, Rate = 40 }
                },
                new TaxResultDTO { AnnualGrossSalary = 40_000, AnnualTaxPaid = 11_000 }
            },
            new object[] {
                1_000, new List<TaxBand>{
                    new TaxBand {LowerLimit = 1_500, Rate = 50}
                },
                new TaxResultDTO {AnnualGrossSalary = 1_000, AnnualTaxPaid = 0 }
            },
            new object[] {
                42_000,
                new List<TaxBand>{},
                new TaxResultDTO {AnnualGrossSalary = 42_000, AnnualTaxPaid = 0 }
            },
        };

    [Theory]
    [MemberData(nameof(SalaryTaxData))]
    public async Task CalculateTax_ReturnsExpectedResults(decimal salary, IEnumerable<TaxBand> taxBands, TaxResultDTO expected)
    {
        _taxBandRepositoryMock.Setup(repo => repo.GetAllTaxBandsAsync()).ReturnsAsync(taxBands);

        var actual = await _taxCalculatorService.CalculateTaxAsync(salary);

        Assert.Equal(expected.AnnualGrossSalary, actual.AnnualGrossSalary);
        Assert.Equal(expected.AnnualNetSalary, actual.AnnualNetSalary);
        Assert.Equal(expected.AnnualTaxPaid, actual.AnnualTaxPaid);
    }
}
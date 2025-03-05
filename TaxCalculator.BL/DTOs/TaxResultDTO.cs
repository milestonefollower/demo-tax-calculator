namespace TaxCalculator.BL.DTOs
{
    public class TaxResultDTO
    {
        public decimal AnnualGrossSalary { get; set; }
        public decimal AnnualTaxPaid { get; set; }
        public decimal AnnualNetSalary => AnnualGrossSalary - AnnualTaxPaid;
        public decimal MonthlyGrossSalary => AnnualGrossSalary / 12m;
        public decimal MonthlyNetSalary => AnnualNetSalary / 12m;
        public decimal MonthlyTaxPaid => AnnualTaxPaid / 12m;
    }
}
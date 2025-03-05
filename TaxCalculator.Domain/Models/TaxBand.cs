namespace TaxCalculator.Domain.Models
{
    public class TaxBand
    {
        public decimal LowerLimit { get; set; }
        public decimal Rate { get; set; }
    }
}
using TaxCalculator.BL.DTOs;

namespace TaxCalculator.BL.CQRS.Commands
{
    public class CalculateTaxCommand : MediatR.IRequest<TaxResultDTO>
    {
        public decimal GrossSalary { get; }

        public CalculateTaxCommand(decimal grossSalary)
        {
            GrossSalary = grossSalary;
        }
    }
}
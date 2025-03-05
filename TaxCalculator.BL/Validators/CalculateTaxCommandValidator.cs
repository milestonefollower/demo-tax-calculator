using FluentValidation;
using TaxCalculator.BL.CQRS.Commands;

namespace TaxCalculator.BL.Validators
{
    public class CalculateTaxCommandValidator : AbstractValidator<CalculateTaxCommand>
    {
        public CalculateTaxCommandValidator()
        {
            RuleFor(command => command.GrossSalary).GreaterThan(0).WithMessage("Gross salary must be greater than 0.");
        }
    }
}
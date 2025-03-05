using FluentValidation;
using TaxCalculator.API.Models;

namespace TaxCalculator.API.Validators;

public class CalculateTaxRequestValidator : AbstractValidator<CalculateTaxRequest>
{
    public CalculateTaxRequestValidator() {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.GrossSalary).GreaterThan(0).WithMessage("Gross salary must be greater than 0.");

    }

}

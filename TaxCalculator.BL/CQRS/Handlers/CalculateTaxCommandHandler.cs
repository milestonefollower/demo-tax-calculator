using MediatR;
using TaxCalculator.BL.CQRS.Commands;
using TaxCalculator.BL.DTOs;
using TaxCalculator.BL.Interfaces;

namespace TaxCalculator.BL.CQRS.Handlers
{
    public class CalculateTaxCommandHandler : IRequestHandler<CalculateTaxCommand, TaxResultDTO>
    {
        private readonly ITaxCalculatorService _taxCalculatorService;

        public CalculateTaxCommandHandler(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService ?? throw new ArgumentNullException(nameof(taxCalculatorService));
        }

        public async Task<TaxResultDTO> Handle(CalculateTaxCommand request, CancellationToken cancellationToken = default)
        {
            return request is null
                ? throw new ArgumentNullException(nameof(request))
                : await _taxCalculatorService.CalculateTaxAsync(request.GrossSalary);
        }
    }
}
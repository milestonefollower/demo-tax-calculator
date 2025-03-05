using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.API.Models;
using TaxCalculator.BL.CQRS.Commands;

namespace TaxCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CalculateTax")]
        public async Task<IActionResult> CalculateTax([FromBody] CalculateTaxRequest request)
        {
            var result = await _mediator.Send(new CalculateTaxCommand(request.GrossSalary));
            return Ok(result);
        }
    }
}
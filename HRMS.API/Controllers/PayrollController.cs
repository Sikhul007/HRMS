using HRMS.Application.DTOs.Payroll;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _service;

        public PayrollController(IPayrollService service)
        {
            _service = service;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate(GeneratePayrollDto dto)
        {
            var result = await _service.GenerateMonthlyPayrollAsync(dto.Month, dto.Year);
            return Ok(result);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport(int month, int year)
        {
            var result = await _service.GetMonthlyReportAsync(month, year);
            return Ok(result);
        }

    }
}

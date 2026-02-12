using HRMS.Application.DTOs.Salary;
using HRMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _service;

        public SalaryController(ISalaryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalaryDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetHistory(int employeeId)
        {
            var result = await _service.GetHistoryAsync(employeeId);
            return Ok(result);
        }

        [HttpGet("current/{employeeId}")]
        public async Task<IActionResult> GetCurrent(int employeeId)
        {
            var result = await _service.GetCurrentSalaryAsync(employeeId);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}

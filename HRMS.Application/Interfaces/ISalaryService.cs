using HRMS.Application.DTOs.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface ISalaryService
    {
        Task<string> CreateAsync(CreateSalaryDto dto);
        Task<SalaryDto?> GetCurrentSalaryAsync(int employeeId);
        Task<List<SalaryDto>> GetHistoryAsync(int employeeId);
    }
}

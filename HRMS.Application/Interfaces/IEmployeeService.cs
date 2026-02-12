using HRMS.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<string> CreateAsync(CreateEmployeeDto dto);
        Task<string> UpdateAsync(int id, UpdateEmployeeDto dto);
        Task<string> DeleteAsync(int id);
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<object> GetAllAsync(EmployeeQueryParams query);
    }
}

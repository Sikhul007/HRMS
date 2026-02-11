using HRMS.Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Department
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<string> CreateAsync(CreateDepartmentDto dto);
        Task<string> UpdateAsync(int id, UpdateDepartmentDto dto);
        Task<string> DeleteAsync(int id);
    }
}

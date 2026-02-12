using HRMS.Application.DTOs.Employee;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> ExistsByCodeAsync(string code);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Employee?> GetByIdAsync(int id);
        Task<(List<Employee>, int totalCount)> GetAllAsync(EmployeeQueryParams query);
        Task<List<Employee>> GetAllActiveAsync();
        Task AddAsync(Employee employee);
        void Update(Employee employee);
        Task SaveChangesAsync();
    }
}

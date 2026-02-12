using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface ISalaryRepository
    {
        Task<List<SalaryStructure>> GetByEmployeeIdAsync(int employeeId);
        Task<SalaryStructure?> GetActiveSalaryAsync(int employeeId);
        Task AddAsync(SalaryStructure salary);
        void Update(SalaryStructure salary);
        Task SaveChangesAsync();
        Task<SalaryStructure?> GetSalaryForMonthAsync(int employeeId, int month, int year);

    }
}

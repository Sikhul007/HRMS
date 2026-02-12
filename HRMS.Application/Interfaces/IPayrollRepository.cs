using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IPayrollRepository
    {
        Task<bool> ExistsAsync(int employeeId, int month, int year);
        Task AddAsync(Payroll payroll);
        Task<List<Payroll>> GetByMonthAsync(int month, int year);
        Task SaveChangesAsync();
    }
}

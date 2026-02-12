using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly HRMSDbContext _context;

        public PayrollRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int employeeId, int month, int year)
        {
            return await _context.Payrolls
                .AnyAsync(p => p.EmployeeId == employeeId &&
                               p.Month == month &&
                               p.Year == year);
        }

        public async Task AddAsync(Payroll payroll)
        {
            await _context.Payrolls.AddAsync(payroll);
        }

        public async Task<List<Payroll>> GetByMonthAsync(int month, int year)
        {
            return await _context.Payrolls
                .Include(p => p.Employee)
                .Where(p => p.Month == month && p.Year == year)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}

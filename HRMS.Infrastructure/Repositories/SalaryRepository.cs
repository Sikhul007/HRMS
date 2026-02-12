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
    public class SalaryRepository : ISalaryRepository
    {
        private readonly HRMSDbContext _context;

        public SalaryRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalaryStructure>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.SalaryStructures
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.EffectiveFrom)
                .ToListAsync();
        }

        public async Task<SalaryStructure?> GetActiveSalaryAsync(int employeeId)
        {
            return await _context.SalaryStructures
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId && s.IsActive);
        }

        public async Task AddAsync(SalaryStructure salary)
        {
            await _context.SalaryStructures.AddAsync(salary);
        }

        public void Update(SalaryStructure salary)
        {
            _context.SalaryStructures.Update(salary);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

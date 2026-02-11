using HRMS.Domain.Entities;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Repositories.Department
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRMSDbContext _context;

        public DepartmentRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Department>> GetAllAsync()
        {
            return await _context.Departments
                .Where(d => d.IsActive)
                .ToListAsync();
        }

        public async Task<Domain.Entities.Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Departments
                .AnyAsync(d => d.Name == name && d.IsActive);
        }

        public async Task AddAsync(Domain.Entities.Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public void Update(Domain.Entities.Department department)
        {
            _context.Departments.Update(department);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

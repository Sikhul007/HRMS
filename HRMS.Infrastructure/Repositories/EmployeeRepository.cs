using HRMS.Application.DTOs.Employee;
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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRMSDbContext _context;

        public EmployeeRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _context.Employees
                .AnyAsync(e => e.EmployeeCode == code && e.IsActive);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email == email && e.IsActive);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<(List<Employee>, int)> GetAllAsync(EmployeeQueryParams query)
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .Where(e => e.IsActive)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
            {
                employees = employees.Where(e =>
                    e.FirstName.Contains(query.Search) ||
                    e.LastName.Contains(query.Search) ||
                    e.Email.Contains(query.Search) ||
                    e.EmployeeCode.Contains(query.Search));
            }

            if (query.DepartmentId.HasValue)
                employees = employees.Where(e => e.DepartmentId == query.DepartmentId);

            if (query.Status.HasValue)
                employees = employees.Where(e => e.EmploymentStatus == query.Status);

            var totalCount = await employees.CountAsync();

            var result = await employees
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (result, totalCount);
        }

        public async Task<List<Employee>> GetAllActiveAsync()
        {
            return await _context.Employees
                .Where(e => e.IsActive)
                .ToListAsync();
        }


        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}

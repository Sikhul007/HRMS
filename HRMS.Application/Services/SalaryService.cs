using HRMS.Application.DTOs.Salary;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;

        public SalaryService(
            ISalaryRepository repository,
            IEmployeeRepository employeeRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
        }

        public async Task<string> CreateAsync(CreateSalaryDto dto)
        {
            if (dto.BasicSalary <= 0)
                return "Basic salary must be greater than zero.";

            if (dto.Allowance < 0 || dto.Bonus < 0 || dto.Deduction < 0)
                return "Allowance, Bonus and Deduction cannot be negative.";

            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                return "Employee not found.";

            var activeSalary = await _repository.GetActiveSalaryAsync(dto.EmployeeId);

            if (activeSalary != null)
            {
                // Prevent overlapping dates
                if (dto.EffectiveFrom <= activeSalary.EffectiveFrom)
                    return "Effective date must be after current active salary.";

                // Close previous salary
                activeSalary.IsActive = false;
                activeSalary.EffectiveTo = dto.EffectiveFrom.AddDays(-1);
                _repository.Update(activeSalary);
            }

            var salary = new SalaryStructure
            {
                EmployeeId = dto.EmployeeId,
                BasicSalary = dto.BasicSalary,
                Allowance = dto.Allowance,
                Bonus = dto.Bonus,
                Deduction = dto.Deduction,
                EffectiveFrom = dto.EffectiveFrom,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(salary);
            await _repository.SaveChangesAsync();

            return "Salary revision created successfully.";
        }


        public async Task<List<SalaryDto>> GetHistoryAsync(int employeeId)
        {
            var salaries = await _repository.GetByEmployeeIdAsync(employeeId);

            return salaries.Select(s => new SalaryDto
            {
                Id = s.Id,
                BasicSalary = s.BasicSalary,
                Allowance = s.Allowance,
                Bonus = s.Bonus,
                Deduction = s.Deduction,
                EffectiveFrom = s.EffectiveFrom,
                EffectiveTo = s.EffectiveTo,
                IsActive = s.IsActive
            }).ToList();
        }


        public async Task<SalaryDto?> GetCurrentSalaryAsync(int employeeId)
        {
            var salary = await _repository.GetActiveSalaryAsync(employeeId);
            if (salary == null) return null;

            return new SalaryDto
            {
                Id = salary.Id,
                BasicSalary = salary.BasicSalary,
                Allowance = salary.Allowance,
                Bonus = salary.Bonus,
                Deduction = salary.Deduction,
                EffectiveFrom = salary.EffectiveFrom,
                EffectiveTo = salary.EffectiveTo,
                IsActive = salary.IsActive
            };
        }
    }
}

using HRMS.Application.DTOs.Employee;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeService(
            IEmployeeRepository repository,
            IDepartmentRepository departmentRepository)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
        }

        public async Task<string> CreateAsync(CreateEmployeeDto dto)
        {
            if (await _repository.ExistsByCodeAsync(dto.EmployeeCode))
                return "Employee code already exists";

            if (await _repository.ExistsByEmailAsync(dto.Email))
                return "Email already exists";

            var department = await _departmentRepository.GetByIdAsync(dto.DepartmentId);
            if (department == null)
                return "Invalid department";

            var employee = new Employee
            {
                EmployeeCode = dto.EmployeeCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                Position = dto.Position,
                AccountNumber = dto.AccountNumber,
                EmploymentStatus = dto.EmploymentStatus,
                JoiningDate = dto.JoiningDate,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();

            return "Employee created successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return "Employee not found";

            var department = await _departmentRepository.GetByIdAsync(dto.DepartmentId);
            if (department == null)
                return "Invalid department";

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Phone = dto.Phone;
            employee.DepartmentId = dto.DepartmentId;
            employee.Position = dto.Position;
            employee.AccountNumber = dto.AccountNumber;
            employee.EmploymentStatus = dto.EmploymentStatus;

            _repository.Update(employee);
            await _repository.SaveChangesAsync();

            return "Employee updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return "Employee not found";

            employee.IsActive = false;

            _repository.Update(employee);
            await _repository.SaveChangesAsync();

            return "Employee deleted successfully";
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FullName = $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                Phone = employee.Phone,
                DepartmentName = employee.Department.Name,
                Position = employee.Position,
                EmploymentStatus = employee.EmploymentStatus,
                JoiningDate = employee.JoiningDate
            };
        }

        public async Task<object> GetAllAsync(EmployeeQueryParams query)
        {
            var (employees, totalCount) = await _repository.GetAllAsync(query);

            var result = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email,
                Phone = e.Phone,
                DepartmentName = e.Department.Name,
                Position = e.Position,
                EmploymentStatus = e.EmploymentStatus,
                JoiningDate = e.JoiningDate
            }).ToList();

            return new
            {
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                Data = result
            };
        }
    }
}

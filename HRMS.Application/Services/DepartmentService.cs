using HRMS.Application.Department;
using HRMS.Application.DTOs.Department;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            }).ToList();
        }

        public async Task<string> CreateAsync(CreateDepartmentDto dto)
        {
            if (await _repository.ExistsByNameAsync(dto.Name))
                return "Department already exists";

            var department = new Domain.Entities.Department
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(department);
            await _repository.SaveChangesAsync();

            return "Department created successfully";
        }

        public async Task<string> UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                return "Department not found";

            department.Name = dto.Name;
            department.Description = dto.Description;

            _repository.Update(department);
            await _repository.SaveChangesAsync();

            return "Department updated successfully";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                return "Department not found";

            department.IsActive = false;

            _repository.Update(department);
            await _repository.SaveChangesAsync();

            return "Department deleted successfully";
        }
    }
}

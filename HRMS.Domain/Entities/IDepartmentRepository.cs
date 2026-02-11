using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
        Task AddAsync(Department department);
        void Update(Department department);
        Task SaveChangesAsync();        
    }
}

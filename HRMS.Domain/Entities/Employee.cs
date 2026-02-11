using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string EmployeeCode { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public int DepartmentId { get; set; }

        public string Position { get; set; } = null!;

        public string? AccountNumber { get; set; }

        public EmploymentStatus EmploymentStatus { get; set; }

        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Department Department { get; set; } = null!;

        public ICollection<SalaryStructure> SalaryStructures { get; set; } = new List<SalaryStructure>();

        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

        public User? User { get; set; }
    }
}

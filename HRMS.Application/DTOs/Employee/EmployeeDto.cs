using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}

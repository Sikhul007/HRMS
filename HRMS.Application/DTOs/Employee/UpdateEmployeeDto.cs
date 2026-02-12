using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string Position { get; set; } = null!;

        public string? AccountNumber { get; set; }

        public EmploymentStatus EmploymentStatus { get; set; }
    }
}

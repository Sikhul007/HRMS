using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Salary
{
    public class CreateSalaryDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public decimal BasicSalary { get; set; }

        public decimal Allowance { get; set; }

        public decimal Bonus { get; set; }

        public decimal Deduction { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; }
    }
}

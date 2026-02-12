using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Salary
{
    public class SalaryDto
    {
        public int Id { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; }

        public decimal TotalSalary =>
        (BasicSalary + Allowance + Bonus) - Deduction;
    }
}

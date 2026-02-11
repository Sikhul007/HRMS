using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class Payroll
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowance { get; set; }

        public decimal Bonus { get; set; }

        public decimal Deduction { get; set; }

        public decimal Tax { get; set; }

        public decimal NetSalary { get; set; }

        public DateTime GeneratedDate { get; set; }

        public PayrollStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public Employee Employee { get; set; } = null!;
    }
}

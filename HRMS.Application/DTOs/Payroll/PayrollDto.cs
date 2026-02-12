using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Payroll
{
    public class PayrollDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal Tax { get; set; }
        public decimal NetSalary { get; set; }
        public PayrollStatus Status { get; set; }
    }
}

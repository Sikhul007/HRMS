using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Payroll
{
    public class GeneratePayrollDto
    {
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}

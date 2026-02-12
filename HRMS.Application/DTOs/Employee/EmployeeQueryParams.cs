using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Employee
{
    public class EmployeeQueryParams
    {
        public string? Search { get; set; }
        public int? DepartmentId { get; set; }
        public EmploymentStatus? Status { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}

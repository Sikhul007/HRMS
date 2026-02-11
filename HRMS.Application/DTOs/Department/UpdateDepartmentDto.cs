using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs.Department
{
    public class UpdateDepartmentDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}

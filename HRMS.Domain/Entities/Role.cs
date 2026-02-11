using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = null!;

        public string? Description { get; set; }

        // Navigation Property
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

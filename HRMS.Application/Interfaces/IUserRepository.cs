using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserWithRolesAsync(string username);
    }
}

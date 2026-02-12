using HRMS.Application.Auth;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HRMSDbContext _context;

        public UserRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserWithRolesAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        }
    }
}

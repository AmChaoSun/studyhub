using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Models;
using StudyHub.Repositories.Interfaces;

namespace StudyHub.Repositories
{
    public class AdminRepository : GenericRepository<AdminUser>, IAdminRepository
    {
        public AdminRepository(StudyHubContext context) : base(context)
        {
        }

        public AdminUser FindUser(string userName)
        {
            return context.AdminUsers
                .Where(x => x.UserName == userName)
                .Include(x => x.Role)
                .FirstOrDefault();
        }
    }
}

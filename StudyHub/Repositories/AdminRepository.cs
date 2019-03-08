using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Repositories
{
    public class AdminRepository : GenericRepository<AdminUser>, IAdminRepository
    {
        public AdminRepository(StudyHubContext context) : base(context)
        {
        }

        public AdminUser FindUser(string userName)
        {
            return Records
                .Where(x => x.UserName == userName)
                .Include(x => x.Role)
                .FirstOrDefault();
        }

        public IEnumerable<User> SearchUsers(UserSearchAttribute info)
        {
            var users = context.Users.Include(x => x.Role)
                                .Search(info.SearchValue)
                                .ApplySort(info.SortString, info.SortOrder);
            if(info.Role != null)
            {
                return users.Where(x => x.Role.Name == info.Role);
            }

            return users;
        }
    }
}

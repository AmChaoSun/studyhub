using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Managers;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StudyHubContext context) : base(context)
        {

        }

        public IEnumerable<User> GetUsers(UserSearchAttribute info)
        {
            var users = Records.Include(x => x.Role)
                .Search(info.SearchValue)
                .ApplySort(info.SortString, info.SortOrder);
            if(info.Role != null)
            {
                users = users.Where(x => x.Role.Name == info.Role);
            }
            return users;
        }

        public User UpdateBasicInfo(User user, UserUpdateDto info)
        {
            var entry = context.Entry(user);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }

        public User UpdateUser(UserUpdateDto info)
        {
            var user = Records
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == info.Id);

            if(user == null)
            {
                throw new CustomDbException("User not Found");
            }

            var entry = context.Entry(user);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }
    }
}

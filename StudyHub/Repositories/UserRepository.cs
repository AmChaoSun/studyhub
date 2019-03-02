using System;
using Microsoft.EntityFrameworkCore;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;

namespace StudyHub.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StudyHubContext context) : base(context)
        {

        }

        public User UpdateBasicInfo(User user, UserUpdateDto info)
        {
            var entry = context.Entry(user);
            entry.CurrentValues.SetValues(info);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }
    }
}

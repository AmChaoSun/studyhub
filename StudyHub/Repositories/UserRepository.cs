using System;
using StudyHub.Models;
using StudyHub.Repositories.Interfaces;

namespace StudyHub.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StudyHubContext context) : base(context)
        {
        }
    }
}

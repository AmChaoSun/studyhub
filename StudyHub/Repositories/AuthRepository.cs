using System;
using System.Linq;
using StudyHub.Models;
using StudyHub.Repositories.Interfaces;

namespace StudyHub.Repositories
{
    public class AuthRepository : GenericRepository<UserAuth>, IAuthRepository
    {
        public AuthRepository(StudyHubContext context) : base(context)
        {
        }

        public User EmailRegister(User user, UserAuth auth)
        {
            if(Records.Where(x => x.IdentityType == "email")
                .Any(x => x.Identifier == auth.Identifier))
            {
                return null;
            }

            context.Users.Add(user);
            auth.UserId = user.Id;
            auth.User = user;
            Records.Add(auth);
            context.SaveChanges();
            return user;
        }
    }
}

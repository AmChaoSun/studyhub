using System;
using System.Linq;
using StudyHub.Models;
using StudyHub.Models.Dtos;
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

        public UserAuthDto GetUserIdByEmail(UserLoginDto user)
        {
            var auth = Records
                .Where(x => x.IdentityType == "email"
                    && x.Identifier == user.Identifier
                    && x.Credential == user.Credential)
                .FirstOrDefault();
            if(auth == null)
            {
                return null;
            }
            return new UserAuthDto { UserId = auth.UserId};
        }
    }
}

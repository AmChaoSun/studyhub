using System;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface IUserAuthRepository : IGenericRepository<UserLoginAuth>
    {
        User EmailRegister(User user, UserLoginAuth auth);
        UserAuthDto GetUserIdByEmail(UserLoginDto user);
    }
}

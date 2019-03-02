using System;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface IAuthRepository : IGenericRepository<UserAuth>
    {
        User EmailRegister(User user, UserAuth auth);
        UserAuthDto GetUserIdByEmail(UserLoginDto user);
    }
}

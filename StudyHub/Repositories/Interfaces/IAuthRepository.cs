using System;
using StudyHub.Models;

namespace StudyHub.Repositories.Interfaces
{
    public interface IAuthRepository : IGenericRepository<UserAuth>
    {
        User EmailRegister(User user, UserAuth auth);
    }
}

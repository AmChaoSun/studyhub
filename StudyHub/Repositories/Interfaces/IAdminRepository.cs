using System;
using System.Collections.Generic;
using StudyHub.Managers;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface IAdminRepository : IGenericRepository<AdminUser>
    {
        AdminUser FindUser(string userName);
        IEnumerable<User> SearchUsers(UserSearchAttribute info);
        User RegisterUser(UserRegisterDto info);
        AdminUser GetAdminById(int adminId);
    }
}

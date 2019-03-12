using System;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface IUserManager
    {
        UserDisplayDto GetUserById(int id);
        UserDisplayDto UpdateUser(UserUpdateDto info);
        UserSearchResultDto GetUsers(UserSearchAttribute info);
        void DeleteUser(int id);
    }
}

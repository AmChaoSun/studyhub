using System;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface IUserManager
    {
        UserDisplayDto GetUserById(int id);
        UserDisplayDto UpdateUser(int id, UserUpdateDto info);
        UserSearchResultDto GetUsers(UserSearchAttribute info);
    }
}

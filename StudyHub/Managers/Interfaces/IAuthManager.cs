using System;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface IAuthManager
    {
        UserDisplayDto RegisterEmailUser(UserEmailRegisterDto info);
    }
}

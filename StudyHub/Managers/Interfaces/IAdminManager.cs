﻿using System;
using StudyHub.Models.Dtos;

namespace StudyHub.Managers.Interfaces
{
    public interface IAdminManager
    {
        string GetToken(AdminLoginDto loginInfo);
        UserSearchResultDto SearchUsers(UserSearchAttribute info);
        UserDisplayDto RegisterUser(UserRegisterDto info);
        AdminDisplayDto GetProfile(int adminId);
    }
}

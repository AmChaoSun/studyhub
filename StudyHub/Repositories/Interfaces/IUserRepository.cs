﻿using System;
using StudyHub.Models;
using StudyHub.Models.Dtos;

namespace StudyHub.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User BasicInfoUpdate(User user, UserUpdateDto info);
    }
}

using System;
using StudyHub.Models;

namespace StudyHub.Repositories.Interfaces
{
    public interface IAdminRepository : IGenericRepository<AdminUser>
    {
        AdminUser FindUser(string userName);
    }
}

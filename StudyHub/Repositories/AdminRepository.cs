using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudyHub.Managers;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Repositories
{
    public class AdminRepository : GenericRepository<AdminUser>, IAdminRepository
    {
        public AdminRepository(StudyHubContext context) : base(context)
        {
        }

        public AdminUser FindUser(string userName)
        {
            return Records
                .Where(x => x.UserName == userName)
                .Include(x => x.Role)
                .FirstOrDefault();
        }

        public User RegisterUser(UserRegisterDto info)
        {
            //check NickName existed or not
            if(context.Users.Any(x => x.NickName == info.NickName))
            {
                throw new CustomDbException("NickName existed.");
            }

            //check loginAuth existed or not
            if(context.UserLoginAuths
                .Any(x => x.IdentityType == info.IdentityType
                    && x.Identifier == info.Identifier))
            {
                throw new CustomDbException("Identifier existed.");
            }

            //check role existed or not
            var role = context.UserRoles
                .Where(x => x.Name == info.Role)
                .FirstOrDefault();
            if(role == null)
            {
                throw new CustomDbException("Role does not exist.");
            }

            //create user
            var user = new User
            {
                NickName = info.NickName,
                IsActive = info.IsActive,
                CreatedOn = DateTime.Now,
            };
            user.RoleId = role.RoleId;
            //eager loading
            user.Role = role;
            //save user
            context.Users.Add(user);
            context.SaveChanges();

            //create loginAuth
            var userLoginAuth = new UserLoginAuth
            {
                Identifier = info.Identifier,
                IdentityType = info.IdentityType,
                Credential = info.Credential,
                InSite = true,
                IsVerified = true,
                UserId = user.Id
            };
            //save loginAuth
            context.UserLoginAuths.Add(userLoginAuth);
            context.SaveChanges();

            return user;
        }

        public IEnumerable<User> SearchUsers(UserSearchAttribute info)
        {
            var users = context.Users.Include(x => x.Role)
                                .Search(info.SearchValue)
                                .ApplySort(info.SortString, info.SortOrder);
            if(info.Role != null)
            {
                return users.Where(x => x.Role.Name == info.Role);
            }

            return users;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudyHub.Managers.Interfaces;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public UserDisplayDto GetUserById(int id)
        {
            var user = userRepository.GetById(id);

            if(user == null)
            {
                throw new CustomDbException("User not Found");
            }

            //data transform
            var displayUser = mapper.Map<User, UserDisplayDto>(user);
            return displayUser;
        }

        public UserSearchResultDto GetUsers(UserSearchAttribute info)
        {
            var users = userRepository.GetUsers(info);
            if (info.PageNumber <= 0)
            {
                info.PageNumber = 1;
            }
            if (info.PageSize <= 0)
            {
                info.PageSize = 10;
            }
            var result = new UserSearchResultDto
            {
                PageSize = info.PageSize,
                TotalPage = users.Count() / info.PageSize
                    + users.Count() % info.PageSize == 0 ? 0 : 1
            };
            result.PageNumber = info.PageNumber > result.TotalPage ? 
                1 : info.PageNumber;
            users = users.Skip((result.PageNumber - 1) * result.PageSize)
                .Take(result.PageSize);
            result.Users = mapper
                .Map<IEnumerable<User>, IEnumerable<UserDisplayDto>>(users);

            return result;
        }

        public UserDisplayDto UpdateUser(int id, UserUpdateDto info)
        {
            var user = userRepository.GetById(id);

            if (user == null)
            {
                throw new CustomDbException("User not Found");
            }

            var updatedUser = userRepository.UpdateBasicInfo(user, info);
            //data transform
            var displayUser = mapper.Map<User, UserDisplayDto>(user);
            return displayUser;
        }
    }
}

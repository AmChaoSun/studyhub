using System;
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

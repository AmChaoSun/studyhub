using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using StudyHub.Managers.Interfaces;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthRepository authRepository;
        private readonly HashHelper hashHelper;
        private readonly IMapper mapper;
        //constructor, DIs
        public AuthManager(IAuthRepository authRepository, 
            IConfiguration configuration,
            IMapper mapper)
        {
            this.authRepository = authRepository;
            this.mapper = mapper;
            hashHelper = new HashHelper(configuration);
        }

        public UserDisplayDto RegisterEmailUser(UserEmailRegisterDto info)
        {
            var user = new User 
            { 
                NickName = info.NickName, 
                CreatedOn = DateTime.Now 
            };
            var auth = new UserAuth 
            { 
                IdentityType = "email",
                Identifier = info.Identifier, 
                Credential = hashHelper.GetHashedData(info.Credential), 
                InSite = true 
            };

            var newUser = authRepository.EmailRegister(user, auth);
            //User validation
            if(newUser == null)
            {
                return null;
            }
            var displayUser = mapper.Map<User, UserDisplayDto>(newUser);
            return displayUser;
        }
    }
}

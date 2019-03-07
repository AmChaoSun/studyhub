using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyHub.Managers.Interfaces;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserAuthRepository authRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        //constructor, DIs
        public AuthManager(IUserAuthRepository authRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            this.authRepository = authRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public string GetInsiteToken(UserLoginDto user)
        {
            user.Credential = new HashHelper(configuration).GetHashedData(user.Credential);
            var userAuth = new UserAuthDto();
            
            //select authentication method
            switch (user.IdentityType)
            {
                case "email": 
                    userAuth = authRepository.GetUserIdByEmail(user);
                    break;
                default: break;
            }

            //if not found
            if (userAuth == null)
            {
                return null;
            }

            return BuildToken(userAuth);
        }

        public UserDisplayDto RegisterEmailUser(UserEmailRegisterDto info)
        {
            var user = new User
            {
                NickName = info.NickName,
                RoleId = 2, 
                CreatedOn = DateTime.Now 
            };
            var auth = new UserLoginAuth 
            { 
                IdentityType = "email",
                Identifier = info.Identifier, 
                Credential = new HashHelper(configuration).GetHashedData(info.Credential), 
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

        //token generator
        private string BuildToken(UserAuthDto auth)
        {
            //get secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //add claims
            var claims = new Claim[]
                {
                    new Claim("userId", auth.UserId.ToString()),
                    new Claim("roles", auth.Role)
                };

            //generate token
            var token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims:claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

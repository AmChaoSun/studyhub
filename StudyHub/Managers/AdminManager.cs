using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyHub.Models;
using StudyHub.Models.Dtos;
using StudyHub.Repositories.Interfaces;
using StudyHub.Utils;

namespace StudyHub.Managers.Interfaces
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository repo;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AdminManager(IAdminRepository repo,
            IMapper mapper,
            IConfiguration configuration)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public string GetToken(AdminLoginDto loginInfo)
        {
            var adminUser = repo.FindUser(loginInfo.UserName);

            //username existed or not
            if(adminUser == null)
            {
                throw new CustomDbException("UserName not existed.");
            }

            //validate password
            if(!new HashHelper(configuration)
                .ValidateHash(loginInfo.Password, adminUser.Password))
            {
                throw new CustomDbException("Wrong password.");
            }

            //build token
            return BuildToken(adminUser);
        }

        public UserSearchResultDto SearchUsers(UserSearchAttribute info)
        {
            var users = repo.SearchUsers(info);
            if (info.PageNumber == 0)
            {
                info.PageNumber = 1;
            }
            if (info.PageSize == 0)
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
            users = users.Skip(result.PageSize * (result.PageNumber - 1))
                        .Take(result.PageSize);
            result.Users = mapper.Map<IEnumerable<User>,
                IEnumerable<UserDisplayDto>>(users);
            return result;
        }

        //token generator
        private string BuildToken(AdminUser admin)
        {
            //get secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //add claims
            var claims = new Claim[] 
                { 
                    new Claim("adminId", admin.AdminId.ToString()),
                    new Claim("role", admin.Role.Name) 
                };
                
            //generate token
            var token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

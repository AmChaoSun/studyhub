using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Managers;
using StudyHub.Managers.Interfaces;
using StudyHub.Models.Dtos;
using StudyHub.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminManager adminManager;
        
        public AdminsController(IAdminManager adminManager)
        {
            this.adminManager = adminManager;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        // POST api/admins/token
        public IActionResult Login(AdminLoginDto LoginInfo)
        {
            //Model validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = adminManager.GetToken(LoginInfo);
                return Ok(token);
            }
            catch(CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("users")]
        public IActionResult SearchUsers(string sortString = "id",
                                        string sortOrder = "ascend",
                                        string searchValue = "",
                                        string role = null,
                                        int pageSize = 10,
                                        int pageNumber = 1)
        {
            //assign search values
            var searchInfo = new UserSearchAttribute
            {
                SortString = sortString,
                SortOrder = sortOrder,
                SearchValue = searchValue,
                Role = role,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = adminManager.SearchUsers(searchInfo);

            return Ok(result);
        }

        [HttpPost("users")]
        public IActionResult RegisterUser(UserRegisterDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = adminManager.RegisterUser(info);
                return Ok(user);
            }
            catch(CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

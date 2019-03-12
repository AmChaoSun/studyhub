﻿using System;
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

    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        // GET api/users/5
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetUser(int id)
        {
            //authorization
            var userId = Int32.Parse(User.FindFirst("userId").Value);
            if (!(userId == id))
            {
                return Forbid();
            }

            try 
            { 
                var user = userManager.GetUserById(id); 
                return Ok(user); 
            }
            catch(CustomDbException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("api/[controller]/profile")]
        public IActionResult GetProfile()
        {
            //authorization
            var userId = Int32.Parse(User.FindFirst("userId").Value);

            try
            {
                var user = userManager.GetUserById(userId);
                return Ok(user);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }

        }

        // PUT api/users/5
        [Route("api/[controller]/{id}")]
        public IActionResult UpdateUser(int id, UserUpdateDto info)
        {
            //authorization
            var userId = Int32.Parse(User.FindFirst("userId").Value);
            if (!(userId == id))
            {
                return Forbid();
            }

            try
            {
                var user = userManager.UpdateUser(id, info);
                return Ok(user);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("api/admin/users")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetUsers(string sortString = "id",
            string sortOrder = "ascend",
            string searchValue = "",
            string role = null,
            int pageSize = 10,
            int pageNumber = 1)
        {
            var info = new UserSearchAttribute
            {
                SortOrder = sortOrder,
                SortString = sortString,
                SearchValue = searchValue,
                Role = role,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = userManager.GetUsers(info);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/admin/users/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminGetUserById(int id)
        {
            try
            {
                var user = userManager.GetUserById(id);
                return Ok(user);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}

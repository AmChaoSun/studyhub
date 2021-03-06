﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Managers.Interfaces;
using StudyHub.Models;
using StudyHub.Models.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager authManager;

        public AuthController(IAuthManager authManager)
        {
            this.authManager = authManager;
        }

        // POST api/auth/insite_token
        [HttpPost("insite_token")]
        public IActionResult InsiteAuth(UserLoginDto user)
        {
            //Model validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = authManager.GetInsiteToken(user);
            if(token == null)
            {
                return BadRequest("Wrong login info.");
            }

            return Ok(token);
        }

        // POST api/auth/emails
        [HttpPost("emails")]
        public IActionResult InsiteRegister(UserEmailRegisterDto info)
        {
            //Model validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //try register
            var newUser = authManager.RegisterEmailUser(info);
            if(newUser == null)
            {
                return BadRequest("Registered user.");
            }
            //return
            return Ok(newUser);
        }

    }
}

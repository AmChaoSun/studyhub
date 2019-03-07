using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Managers.Interfaces;
using StudyHub.Models.Dtos;
using StudyHub.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}")]
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

        // PUT api/users/5
        [HttpPut("{id}")]
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
    }
}

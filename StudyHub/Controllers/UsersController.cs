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
            if (!Int32.TryParse(User.FindFirst("userId").Value, out int userId))
            {
                return Forbid();
            };
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
            if (!Int32.TryParse(User.FindFirst("userId").Value, out int userId))
            {
                return Forbid();
            };

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
        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult UpdateUser(int id, UserUpdateDto info)
        {
            //authorization
            if (!Int32.TryParse(User.FindFirst("userId").Value, out int userId))
            {
                return Forbid();
            };
            if (userId != id)
            {
                return Forbid();
            }
            //info id validation
            if (id != info.Id)
            {
                return BadRequest("Id not match.");
            }

            try
            {
                var user = userManager.UpdateUser(info);
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

        [HttpDelete]
        [Route("api/admin/users/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminDeleteUser(int id)
        {
            try
            {
                userManager.DeleteUser(id);
                return Ok();
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }
    
        [HttpPut]
        [Route("api/admin/users/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminUpdateUser(int id, UserUpdateDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != info.Id)
            {
                return BadRequest("Id not match.");
            }
            try
            {
                var user = userManager.UpdateUser(info);
                return Ok(user);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/admin/lecturers/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminGetLectureById(int id)
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

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        [Route("api/admin/enrolls/students/{studentId}")]
        public IActionResult GetEnrolledCourses(int studentId)
        {
            try
            {
                var courses = userManager.GetEnrolledCourses(studentId);
                return Ok(courses);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [Route("api/admin/enrolls")]
        public IActionResult AdminEnrollStudent(AdminEnrollInfo info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                userManager.StudentEnrollCourse(info.StudentId, info.CourseId);
                return Ok();
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminOnly")]
        [Route("api/admin/enrolls/student{studentId}/course{courseId}")]
        public IActionResult AdminUnenrollStudent(int studentId, int courseId)
        {
            try
            {
                userManager.StudentUnenrollCourse(studentId, courseId);
                return Ok();
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

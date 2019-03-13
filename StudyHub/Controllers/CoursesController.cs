using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Managers.Interfaces;
using StudyHub.Models.Dtos;
using StudyHub.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyHub.Controllers
{
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseManager courseManager;

        public CoursesController(ICourseManager courseManager)
        {
            this.courseManager = courseManager;
        }

        [HttpGet]
        [Route("api/courses")]
        [AllowAnonymous]
        public IActionResult GetCourses()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/courses/{courseId}")]
        [AllowAnonymous]
        public IActionResult GetCourseById(int courseId)
        {
            try 
            { 
                var course = courseManager.GetCourseById(courseId);
                return Ok(course);
            }
            catch(CustomDbException e) 
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        [Route("api/courses")]
        public IActionResult CreateCourse(CourseRegisterDto course)
        {
            var userId = Int32.Parse(User.FindFirst("userId").Value);

            //assign publisherId
            if(userId != course.PublisherId)
            {
                return Forbid();
            }

            try
            {
                var createdCourse = courseManager.CreateCourse(course);
                return Ok(createdCourse);
            }
            catch(CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        [Route("api/courses/{courseId}")]
        public IActionResult UpdateCourse(int courseId, CourseUpdateDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //authentication
            var userId = Int32.Parse(User.FindFirst("userId").Value);
            if(info.PublisherId != userId)
            {
                return Forbid();
            }

            try
            {
                var course = courseManager.UpdateCourse(info);
                return Ok(course);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }

        }
        
        [HttpDelete]
        [Route("api/courses/{courseId}")]
        public IActionResult DeleteCourse(int courseId)
        {
            var userId = Int32.Parse(User.FindFirst("userId").Value);

            try
            {
                courseManager.DeleteCourse(courseId, userId);
                return Ok();
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/admin/courses")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminGetAllCourses()
        {
            var courses = courseManager.AdminGetAllCourses();
            return Ok(courses);
        }

        [HttpGet]
        [Route("api/admin/courses/{courseId}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminGetCourseById(int courseId)
        {
            var course = courseManager.AdminGetCourseById(courseId);
            if(course == null)
            {
                return BadRequest("Course does not found.");
            }
            return Ok(course);
        }

        [HttpPost]
        [Route("api/admin/courses")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminRegisterCourse(CourseRegisterDto courseInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var course = courseManager.AdminRegisterCourse(courseInfo);
                return Ok(course);
            }
            catch(CustomDbException e)
            {
                return BadRequest(e.Message);
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut]
        [Route("api/admin/courses/{courseId}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminUpdateCourse(int courseId, CourseUpdateDto info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(courseId != info.CourseId)
            {
                return BadRequest("Unmatched course id.");
            }
            try
            {
                var course = courseManager.UpdateCourse(info);
                return Ok(course);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete]
        [Route("api/admin/courses/{courseId}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminDeleteCourse(int courseId)
        {
            try
            {
                courseManager.AdminDeleteCourse(courseId);
                return Ok();
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        //GET api/admin/enrolls/courses/id
        //get enrolled students for course id
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        [Route("api/admin/enrolls/courses/{courseId}")]
        public IActionResult GetEnrolledStudents(int courseId)
        {
            try
            {
                var students = courseManager.GetEnrolledStudents(courseId);
                return Ok(students);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

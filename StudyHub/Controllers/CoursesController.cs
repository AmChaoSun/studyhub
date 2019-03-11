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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseManager courseManager;

        public CoursesController(ICourseManager courseManager)
        {
            this.courseManager = courseManager;
        }

        // GET: api/courses
        // Get courses list(search)
        [HttpGet]
        [Route("api/courses")]
        [AllowAnonymous]
        public IActionResult GetCourses()
        {
            return Ok();
        }

        // GET api/courses/5
        // Get course by id

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

        // POST api/courses
        // Post a course
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

        // PUT api/courses/5
        // Update a course
        [HttpPut]
        [Route("api/courses/{courseId}")]
        public IActionResult UpdateCourse(int courseId, CourseUpdateDto info)
        {
            var userId = Int32.Parse(User.FindFirst("userId").Value);

            //assign publisherId
            info.PublisherId = userId;

            try
            {
                var course = courseManager.UpdateCourse(courseId, info);
                return Ok(course);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE api/courses/5
        // Delete a course
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

        //[HttpGet("{courseId}/enrolls")]
        //[Route("api/courses")]
        //public IActionResult GetEnrolledStudents(int courseId) 
        //{
        //    var userId = Int32.Parse(User.FindFirst("userId").Value);

        //    try
        //    {
        //        var students = courseManager.GetEnrolledStudents(courseId, userId);
        //        return Ok(students);
        //    }
        //    catch (CustomDbException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

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
            catch(UnauthorizedAccessException e)
            {
                return Forbid();
            }


        }
    }
}

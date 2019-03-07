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
        [AllowAnonymous]
        public IActionResult GetCourses()
        {
            return Ok();
        }

        // GET api/courses/5
        // Get course by id
        [HttpGet("{courseId}")]
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
        public IActionResult CreateCourse(CourseRegisterDto course)
        {
            var userId = Int32.Parse(User.FindFirst("userId").Value);

            //assign publisherId
            course.PublisherId = userId;

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
        [HttpPut("{courseId}")]
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
        [HttpDelete("{courseId}")]
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

        [HttpGet("{courseId}/enrolls")]
        public IActionResult GetEnrolledStudents(int courseId) 
        {
            var userId = Int32.Parse(User.FindFirst("userId").Value);

            try
            {
                var students = courseManager.GetEnrolledStudents(courseId, userId);
                return Ok(students);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

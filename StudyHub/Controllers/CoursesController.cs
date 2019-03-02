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
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetCourseById(int id)
        {
            try 
            { 
                var course = courseManager.GetCourseById(id);
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
            var userId = Int32.Parse(User.FindFirst("UserId").Value);

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
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, CourseUpdateDto info)
        {
            var userId = Int32.Parse(User.FindFirst("UserId").Value);

            //assign publisherId
            info.PublisherId = userId;

            try
            {
                var course = courseManager.UpdateCourse(id, info);
                return Ok(course);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE api/courses/5
        // Delete a course
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var userId = Int32.Parse(User.FindFirst("UserId").Value);

            try
            {
                courseManager.DeleteCourse(id, userId);
                return Ok();
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/students")]
        public IActionResult GetStudentsByCourse(int id) 
        {
            var userId = Int32.Parse(User.FindFirst("UserId").Value);

            try
            {
                var students = courseManager.GetStudentsByCourse(id, userId);
                return Ok(students);
            }
            catch (CustomDbException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

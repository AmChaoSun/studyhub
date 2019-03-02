using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
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
            return Ok();
        }

        // POST api/courses
        // Post a course
        [HttpPost]
        public void CreateCourse([FromBody]string value)
        {
        }

        // PUT api/courses/5
        // Update a course
        [HttpPut("{id}")]
        public void UpdateCourse(int id, [FromBody]string value)
        {
        }

        // DELETE api/courses/5
        // Delete a course
        [HttpDelete("{id}")]
        public void DeleteCourse(int id)
        {
        }

        [HttpGet("{id}/students")]
        public IActionResult GetStudentsByCourse(id) 
        {
            return Ok();
        }

    }
}

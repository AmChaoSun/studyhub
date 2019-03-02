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
    [Authorize]
    public class EnrollsController : ControllerBase
    {
        // GET: api/enrolls
        //Get all the enrolled courses for user
        [HttpGet]
        public IActionResult GetEnrolledCourses()
        {
            return Ok();
        }

        // POST api/enrolls
        //enroll a course
        [HttpPost]
        public void Post([FromBody]int courseId)
        {
        }

        // DELETE api/enrolls/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

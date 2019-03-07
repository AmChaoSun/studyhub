using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudyHub.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public TestsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET api/tests
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("hash/{code}")]
        public IActionResult GetHashedCode(string code)
        {
            var hashedCode = new HashHelper(configuration).GetHashedData(code);
            return Ok(hashedCode);
        }

        [HttpGet("policies")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult TestPolicy()
        {
            return Ok();
        }

    }
}

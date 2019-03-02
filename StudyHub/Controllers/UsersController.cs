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
    public class UsersController : Controller
    {
        // GET api/users/5
        [HttpGet("{id}")]
        public string GetUser(int id)
        {
            return "value";
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody]string value)
        {
        }
    }
}

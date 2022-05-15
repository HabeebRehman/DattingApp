using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
            
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string>GetSecret()
        {
            return "Secret Key";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser>GetNotFound()
        {
            var thing =_context.users.Find(-1);
            if(thing==null) return NotFound();
             return Ok(thing);
        }
        [HttpGet("server-error")]
        public ActionResult<string>GetServerError()
        {
            var thing =_context.users.Find(-1);
            var ThingsToReturn=thing.ToString();
            return ThingsToReturn;
        }
        [HttpGet("bad-request")]
        public ActionResult<string>GetBadRequest()
        {
           return BadRequest("This  is not a good request");
        }
    }
}
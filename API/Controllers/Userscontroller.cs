using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Userscontroller : BaseApiController
    {
        private readonly DataContext _context;

        public Userscontroller(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
        {
            return await _context.users.ToListAsync();
            
        }
      
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUser>>GetUsers(int id)
        {
            return await _context.users.FindAsync(id);
            
        }

       

    
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Userscontroller : Controller
    {
        private readonly DataContext _context;

        public Userscontroller(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
        {
            return await _context.users.ToListAsync();
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>>GetUsers(int id)
        {
            return await _context.users.FindAsync(id);
            
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
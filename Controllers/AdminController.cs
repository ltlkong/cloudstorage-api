using ltl_webdev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_webdev.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        public AdminController(WebDevDbContext context) : base(context)
        {
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            ICollection<User> users = await _context.Users.Include(user => user.Roles).ToListAsync();

            return Ok(new { msg="All users", users});
        }
    }
}

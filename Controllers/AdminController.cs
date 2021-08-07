using ltl_cloudstorage.Dtos;
using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_cloudstorage.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly UserService _userService;
        public AdminController(CSDbContext context, UserService userService) : base(context)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            ICollection<User> users = await _userService.GetAll();

            return Ok(new { msg="All users", users});
        }
    }
}

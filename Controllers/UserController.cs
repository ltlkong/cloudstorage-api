using ltl_webdev.Dtos;
using ltl_webdev.Models;
using ltl_webdev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_webdev.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(WebDevDbContext context, UserService userService) : base(context)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Get user and information.
            User user = GetCurrentUser();

            return Ok(new { msg="Your infomation" ,user});
        }
        [HttpPost("createinfo")]
        public async Task<IActionResult> CreateInfo(UserDto userDto)
        {
            try
            {
                await _userService.CreateInfo(GetCurrentUser().Id,userDto);
            }
            catch
            {
                return BadRequest();
            }

            UserInfo userInfo = await _context.UserInfos.FindAsync(GetCurrentUser().Id);

            return CreatedAtAction("CreateInfo",userInfo);       
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

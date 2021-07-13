using ltl_codeplatform.Dtos;
using ltl_codeplatform.Models;
using ltl_codeplatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_codeplatform.Controllers
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
        public async Task<IActionResult> Get()
        {
            // Get user and information.
            User user = GetCurrentUser();
            UserInfo userInfo = await _context.UserInfos.FindAsync(user.Id);

            return Ok(new { msg="Your infomation" ,user, userInfo});
        }
        [HttpPost("createinfo")]
        public async Task<IActionResult> CreateInfo(UserDto userDto)
        {
            try
            {
                await _userService.CreateInfoAsync(GetCurrentUser().Id,userDto);
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

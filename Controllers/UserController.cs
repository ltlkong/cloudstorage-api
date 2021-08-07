using ltl_cloudstorage.Dtos;
using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_cloudstorage.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(CSDbContext context, UserService userService) : base(context)
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
        [HttpPut("updateInfo")]
        public async Task<IActionResult> UpdateInfo(PropValueDto propValue)
        {       
            try
            {
                bool isUpdated = await _userService.UpdateInfoAsync(GetCurrentUser(), propValue.Prop, propValue.Value);

                if (!isUpdated)
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }    

            return Ok(new { msg = "Updated" });
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

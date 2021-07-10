using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ltl_webdev.Models;
using ltl_webdev.Services;
using ltl_webdev.Dto;

namespace ltl_webdev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserService _userService;
        public AuthController(WebDevDbContext context, UserService userService) : base(context)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginDto loginDto)
        {
            try
            {
                await _userService.CreateAsync(loginDto);
            } catch
            {
                return BadRequest();
            }

            return Ok(new { msg = "Registered successfully!", userName = loginDto.Name });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            bool IsCorrectPwd = await _userService.VerifyPwdAsync(loginDto);

            if (!IsCorrectPwd)
                return Unauthorized(new { message="Incorrect password"});

            User user = await _userService.GetByNameAsync(loginDto.Name);

            //Generate token

            return Ok(new { msg="Signed in successfully!", userName=user.Name, token="123"});
        }
    }
    
}

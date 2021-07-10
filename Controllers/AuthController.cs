using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ltl_webdev.Models;
using ltl_webdev.Services;
using ltl_webdev.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ltl_webdev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly JwtService _jwtService;
        public AuthController(WebDevDbContext context, UserService userService, JwtService jwtService) : base(context, userService)
        {
            _jwtService = jwtService;
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

            // Claim user
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // Generate token
            var jwtToken = _jwtService.GenerateToken(user.Name, user.Id.ToString(), DateTime.Today.AddDays(7), claims);
            
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(new { msg="Signed in successfully!", userName=user.Name, token });
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(CurrentUser());
        }
    }
    
}

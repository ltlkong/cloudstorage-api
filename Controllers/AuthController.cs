using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ltl_webdev.Models;
using ltl_webdev.Services;
using ltl_webdev.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ltl_webdev.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly JwtService _jwtService;
        private readonly AuthService _authService;
        public AuthController(WebDevDbContext context, AuthService authService, JwtService jwtService) : base(context)
        {
            _jwtService = jwtService;
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginDto loginDto)
        {
            bool isValid = _authService.ValidatePassword(loginDto.Password)
                && _authService.ValidateEmail(loginDto.Email);

            if (!isValid)
                return ValidationProblem();

            try
            {
                await _authService.CreateAsync(loginDto);
            } catch
            {
                var descriptor = new ValidationProblemDetails();
                descriptor.Detail = "Name or Email has been registered already.";

                return ValidationProblem(descriptor);
            }

            return Ok(new { msg = "Registered successfully.", name = loginDto.Name });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            bool IsCorrectPwd = await _authService.VerifyPwdAsync(loginDto);

            if (!IsCorrectPwd)
                return ValidationProblem();

            User user = await _authService.GetByNameAsync(loginDto.Name)
                ?? await _authService.GetByEmailAsync(loginDto.Email);

            // Claim user
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // Generate token
            var jwtToken = _jwtService.GenerateToken(user.Email, user.Id.ToString(), DateTime.Today.AddDays(7), claims);
            
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            DateTime expiration = jwtToken.ValidTo;

            return Ok(new { msg="Signed in successfully.", name=user.Email, token, expiration });
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            // Get user and information.
            User user = GetCurrentUser();
            UserInfo userInfo = await _context.UserInfos.FindAsync(user.Id);

            return Ok(new { msg = "Your infomation", user, userInfo });
        }
    }
    
}

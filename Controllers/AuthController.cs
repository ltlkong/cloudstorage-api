using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using ltl_cloudstorage.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ltl_cloudstorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly JwtService _jwtService;
        private readonly AuthService _authService;
        public AuthController(CSDbContext context, AuthService authService, JwtService jwtService) : base(context)
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
                await _authService.RegisterAsync(loginDto);
            } catch
            {
                return ValidationProblem();
            }

            return CreatedAtAction("Register",new { msg = "Registered successfully.", name = loginDto.Name });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            bool IsCorrectPwd = await _authService.VerifyPwdAsync(loginDto);

            if (!IsCorrectPwd)
                return ValidationProblem();

            User user = await _authService.GetByEmailAsync(loginDto.Email);

            if (user == null)
                return BadRequest(new { message = "Some errors happened." });

            // Update last login time
            user.LastLoginAt = DateTime.Now;
            await _context.SaveChangesAsync();

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

            return Ok(new { msg="Signed in successfully.", email=user.Email, token, expiration });
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            // Get user and information.
            User user = GetCurrentUser();

            var authUser = new
            {
                name=user.Name,
                displayName=user.DisplayName,
                roles=user.Roles,
                avatar=user.Avatar,
				memberships = user.Memberships
            };

            return Ok(new { msg = "Your infomation", user=authUser });
        }
        [HttpGet("check")]
        public async Task<IActionResult> Check(string email, string name)
        {
            List<string> invalidData = new List<string>();

            if (email != null)
            {
                if (!_authService.ValidateEmail(email))
                    invalidData.Add("email");

                // Check is email in the database
                User user = await _authService.GetByEmailAsync(email);

                if (user != null)
                    invalidData.Add("email");

            }
            if (name != null)
            {
                // Check is email in the database
                User user = await _authService.GetByNameAsync(name);

                if (user != null)
                    invalidData.Add("name");
            }

            if (email == null && name == null)
                return BadRequest();

            return Ok(new { msg="These data are invalid.", invalidData });
        }
    }
    
}

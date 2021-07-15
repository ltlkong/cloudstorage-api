using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ltl_pf.Models;
using ltl_pf.Services;
using ltl_pf.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ltl_pf.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly JwtService _jwtService;
        private readonly AuthService _authService;
        public AuthController(PFDbContext context, AuthService authService, JwtService jwtService) : base(context)
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

            return Ok(new { msg = "Your infomation", user });
        }
        [HttpPost("email")]
        public async Task<IActionResult> GetEmail(string email)
        {
            // Check is email in the database
            User user = await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower().Equals(email.ToLower()));
   
            if (user != null)
            {
                var descriptor = new ValidationProblemDetails(new Dictionary<string,string[]>(){
                    {"Email", new string[]{"isOccupied" } }
                });
                descriptor.Detail = "Name or Email has been registered already.";
   
                return ValidationProblem(descriptor);
            }
               
            return Ok(new { msg = "The email is valid", email });
        }
    }
    
}

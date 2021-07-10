using ltl_webdev.Models;
using ltl_webdev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_webdev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly WebDevDbContext _context;
        protected readonly UserService _userService;
        public BaseController(WebDevDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;     
        }
        protected User CurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            // Get token from header
            string token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            // Convert token
            var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            //Get user
            int userId = Int32.Parse(securityToken.Issuer);
            User user = _context.Users.Include(user => user.Roles).FirstOrDefault(user => user.Id == userId); 

            return user;
        }
    }
}

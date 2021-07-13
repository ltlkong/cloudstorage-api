using ltl_codeplatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace ltl_codeplatform.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly WebDevDbContext _context;
        public BaseController(WebDevDbContext context)
        {
            _context = context;
        }
        protected User GetCurrentUser()
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
            User user = _context.Users.Include(user => user.Roles).FirstOrDefault(user => user.Id==userId);            

            return user;
        }
    }
}

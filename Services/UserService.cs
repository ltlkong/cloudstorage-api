using ltl_webdev.Dtos;
using ltl_webdev.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ltl_webdev.Services
{
    public class UserService : BaseService
    {
        public UserService(WebDevDbContext context) : base(context)
        {
        }

        public async Task CreateAsync(LoginDto loginDto)
        {
            User user = new User
            {
                Name = loginDto.Name,
                Email = loginDto.Email,
            };

            //Set hashed password
            user.PasswordHash = md5Encode(loginDto.Password);

            //Default values
            user.IsConfirmed = false;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> VerifyPwdAsync(LoginDto loginDto) 
        {
            User user = await this.GetByNameAsync(loginDto.Name);

            return md5Encode(loginDto.Password) == user.PasswordHash;
        }
        public async Task<User> GetByNameAsync(string name)
        {
            User user = await _context.Users.Include(user => user.Roles).FirstOrDefaultAsync(user => user.Name.Equals(name));

            return user;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            User user = await _context.Users.Include(user => user.Roles).FirstOrDefaultAsync(user => user.Email.Equals(email));

            return user;
        }
        #region helpers
        //Encode a string by using md5
        private string md5Encode(string str)
        {
            //Encoding password using md5
            var md5 = new MD5CryptoServiceProvider();
            var strByte = Encoding.ASCII.GetBytes(str);
            string encodedStr = Convert.ToBase64String(md5.ComputeHash(strByte));

            return encodedStr;
        }
        #endregion
    }
}

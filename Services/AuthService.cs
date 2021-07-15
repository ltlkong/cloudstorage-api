using ltl_pf.Dtos;
using ltl_pf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ltl_pf.Services
{
    public class AuthService : BaseService
    {
        public AuthService(PFDbContext context) : base(context)
        {
        }

        public async Task RegisterAsync(LoginDto loginDto)
        {
            User user = new User
            {
                Name = loginDto.Name,
                Email = loginDto.Email,
            };

            // Default values
            user.CreatedAt = DateTime.Now;

            // Set hashed password
            user.PasswordHash = Md5Encode(loginDto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> VerifyPwdAsync(LoginDto loginDto) 
        {
            User user = await this.GetByNameAsync(loginDto.Name) ?? await this.GetByEmailAsync(loginDto.Email);

            if (user == null)
                return false;

            return Md5Encode(loginDto.Password) == user.PasswordHash;
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
        public bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^\w+\@\w+\.\w+$");

            return regex.IsMatch(email);
        }
        public bool ValidatePassword(string password)
        {
            Regex regex = new Regex(@"^\w{8,}$");

            return regex.IsMatch(password);
        }
        #region helpers
        //Encode a string by using md5
        private string Md5Encode(string str)
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

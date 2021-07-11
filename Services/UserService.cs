using ltl_webdev.Dtos;
using ltl_webdev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_webdev.Services
{
    public class UserService : BaseService
    {
        public UserService(WebDevDbContext context) : base(context)
        {
        }

        public async Task CreateInfo(int userId ,UserDto userDto)
        {
            UserInfo userInfo = new UserInfo()
            {
                Id=userId,
                Introduction = userDto.Introduction
            };

            //Default values
            userInfo.CreatedAt = DateTime.Now;
            userInfo.UpdatedAt = DateTime.Now;
            userInfo.Reputation = 50;

            await _context.UserInfos.AddAsync(userInfo);
            await _context.SaveChangesAsync();
        }
    }
}

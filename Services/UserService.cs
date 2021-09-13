﻿using ltl_cloudstorage.Dtos;
using ltl_cloudstorage.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Services
{
    public class UserService : BaseService
    {
        public UserService(CSDbContext context) : base(context)
        {
        }
        public async Task<ICollection<User>> GetAllAsync()
        {
            ICollection<User> users = await _context.Users.Include(user => user.Roles).ToListAsync();

            return users;
        }
        public async Task CreateInfoAsync(int userId ,UserDto userDto)
        {
            UserInfo userInfo = new UserInfo()
            {
                Id=userId,
                Introduction = userDto.Introduction
            };

            //Default values
            userInfo.UpdatedAt = DateTime.Now;
            userInfo.Reputation = 50;

            await _context.UserInfos.AddAsync(userInfo);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateInfoAsync(User currentUser, string prop, string value)
        {
            prop = WordFirstCharToUpper(prop);
            string[] editableProps =
            {
                "Description", "Email", "Name", "Reputation", "Introduction","DisplayName"
            };
           
            UserInfo userInfo = await _context.UserInfos.FindAsync(currentUser.Id);

            if (!editableProps.Contains(prop))
                throw new InvalidOperationException("Invalid property.");

            int nullCounter = 0;

            // Check if the prop exits in User model
            Type userType = currentUser.GetType();

            PropertyInfo userProp = userType.GetProperty(prop);

            if (userProp != null)
            {
                userProp.SetValue(currentUser, value);
            }
            else
            {
                nullCounter++;
            }

            // Check if the prop exits in UserInfo model
            Type userInfoType = userInfo.GetType();

            PropertyInfo userInfoProp = userInfoType.GetProperty(prop);
            if (userInfoProp != null)
            {
                if(prop.Equals("Reputation"))
                {
                    userInfoProp.SetValue(userInfo, Int32.Parse(value));
                }
                else
                {
                    userInfoProp.SetValue(userInfo, value);
                }     
            }
            else
            {
                nullCounter++;
            }

            if(nullCounter == 2)
            {
                return false;
            }

            userInfo.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }
        #region helpers
        // Take a string split by white space. E.g. display name => DisplayName
        private string WordFirstCharToUpper(string str)
        {
            string[] strArray = str.Split(' ');
            TextInfo tf = new CultureInfo("en-US").TextInfo;
            
            for(int i=0; i<strArray.Length; i++)
            {
                strArray[i] = tf.ToTitleCase(strArray[i]);
            }

            string result = string.Join("",strArray);

            return result;           
        }
        #endregion
    }
}

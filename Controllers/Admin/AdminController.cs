using ltl_cloudstorage.Dtos;
using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ltl_cloudstorage.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_cloudstorage.Admin.Controllers
{
    public class AdminController : BaseController
    {
        protected readonly UserService _userService;
		protected readonly StorageService _storageService;
        public AdminController(CSDbContext context
				, UserService userService
				, StorageService storageService) : base(context)
        {
            _userService = userService;
			_storageService = storageService;
        }

    }
}

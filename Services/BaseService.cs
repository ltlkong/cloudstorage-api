﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ltl_webdev.Models;

namespace ltl_webdev.Services
{
    public class BaseService
    {
        protected readonly WebDevDbContext _context;
        public BaseService(WebDevDbContext context)
        {
            _context = context;
        }
    }
}

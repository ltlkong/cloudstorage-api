using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ltl_cloudstorage.Models;

namespace ltl_cloudstorage.Services
{
    public class BaseService
    {
        protected readonly CSDbContext _context;
        public BaseService(CSDbContext context)
        {
            _context = context;
        }
    }
}

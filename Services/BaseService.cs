using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ltl_pf.Models;

namespace ltl_pf.Services
{
    public class BaseService
    {
        protected readonly PFDbContext _context;
        public BaseService(PFDbContext context)
        {
            _context = context;
        }
    }
}

using ltl_cloudstorage.Models;
using Microsoft.EntityFrameworkCore;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Controllers.Show
{
    public class ShowController : BaseController
    {
		protected StorageService _storageService;

        public ShowController(CSDbContext context, StorageService storageService) : base(context){
			_storageService = storageService;
   		}
	}
}


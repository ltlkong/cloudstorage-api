using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Controllers
{
    public class ShowController : BaseController
    {
		private readonly StorageService _storageService;

        public ShowController(CSDbContext context, StorageService storageService) : base(context){
			_storageService = storageService;
   		}

        [AllowAnonymous]
        [HttpGet("file")]
        public async Task<IActionResult> Get(string name)
        {
            LtlFile file = (await _storageService.SearchFilesByNameAsync(name)).First();
            Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);
            string mimeType = _storageService.GetFileType(file.Name);
            
        
            Response.Headers.Add("Access-Control-Expose-Headers", "content-disposition");

            return File(fileBytes, mimeType, file.Name);
        }

        [AllowAnonymous]
        [HttpPost("file")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;

            if (file.Length > 0)
            {
                await _storageService.StoreAsync(file, 1, null);
            }

            return CreatedAtAction("PostFile", new { size, fileName = file.FileName });
        }
	}
}


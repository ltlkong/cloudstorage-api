using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;

namespace ltl_cloudstorage.Admin.Controllers 
{
	[Route("api/Adimn/File")]
	[Authorize("admin")]
	[ApiController]
	public class AdminFileController : AdminController
	{
        public AdminFileController(CSDbContext context
				, UserService userService
				, StorageService storageService) : base(context, userService, storageService)
		{}

        // GET: api/<StorageController>
        [HttpGet]
        public async Task<IActionResult> Get(int fileid)
        {
            LtlFile file = await _storageService.GetFileByIdAsync(fileid);
            Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);
            string mimeType = _storageService.GetFileType(file.Name);

			// For front-end getting file name
            Response.Headers.Add("Access-Control-Expose-Headers", "content-disposition");

            return File(fileBytes, mimeType, file.Name);
        }

	}
}

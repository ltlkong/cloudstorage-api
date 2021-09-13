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

namespace ltl_cloudstorage.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class ShowController : BaseController
    {
		private readonly StorageService _storageService;

        public ShowController(CSDbContext context, StorageService storageService) : base(context){
			_storageService = storageService;
   		}

        [HttpGet("file/search")]
        public async Task<IActionResult> GetFileBy(string type, string value)
        {
			if(!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(value))
			{
            	ICollection<LtlFile> files = new List<LtlFile>();

				switch(type)
				{
					case "name":
            			files = await _storageService.SearchFilesByNameAsync(value);
						break;
					case "id":
						LtlFile file = await _storageService.SearchFileByUniqueIdAsync(value);
						if(file != null)
							files.Add(file);
						break;
					default:
						break;
				}

				if(files.Count == 0)
					return NotFound();
				return Ok(files);
			}

			return BadRequest();
        }

		[HttpGet("file/entity")]
		public async Task<IActionResult> GetFileEntityByUniqueId(string uniqueId)
		{
			LtlFile file = await _storageService.SearchFileByUniqueIdAsync(uniqueId);

			Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);

			string mimeType = _storageService.GetFileType(file.Name);

			return File(fileBytes, mimeType,file.Name);
		}

		[HttpGet("file")]
		public async Task<IActionResult> GetAllFiles()
		{
			List<LtlFile> files = await _context.LtlFiles.Where(f => !f.isDeleted).ToListAsync();

			return Ok(files);
		}

        [HttpPost("file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            long size = file.Length;

            if (file.Length > 0)
            {
                await _storageService.StoreAsync(file, 1, null);
            }

            return CreatedAtAction(nameof(UploadFile), new { size, fileName = file.FileName });
        }

		[HttpGet("test")]
		public async Task<IActionResult> Test()
		{
			return Ok(new {test="hi"});
		}
	}
}


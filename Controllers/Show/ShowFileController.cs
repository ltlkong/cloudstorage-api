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
	[Route("api/show/file")]
	[ApiController]
    public class ShowFileController : ShowController
    {
		private const long MaxFileSize = 1L * 1024L * 1024L * 1024L;

        public ShowFileController(CSDbContext context, StorageService storageService) : base(context, storageService){
   		}

        [HttpGet("search")]
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

		[HttpGet("{uniqueId}/entity")]
		public async Task<IActionResult> GetFileEntityByUniqueId(string uniqueId)
		{
			LtlFile file = await _storageService.SearchFileByUniqueIdAsync(uniqueId);

			if(file == null)
				return NotFound();

			Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);

			string mimeType = _storageService.GetFileType(file.Name);

			return File(fileBytes, mimeType,file.Name);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFiles()
		{
			List<LtlFile> files = await _context.LtlFiles.Where(f => !f.isDeleted).ToListAsync();

			return Ok(files);
		}

		[HttpGet("{uniqueId}")]
		public async Task<IActionResult> GetFileByUniqueId(string uniqueId)
		{
			LtlFile file = await _storageService.SearchFileByUniqueIdAsync(uniqueId);

			if(file == null)
				return NotFound();

			return Ok(file);
		}

		// Large file ref: https://stackoverflow.com/questions/62502286/uploading-and-downloading-large-files-in-asp-net-core-3-1
		[RequestSizeLimit(MaxFileSize)]
		[RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            long size = file.Length;
			LtlFile dbFile = null;

            if (file.Length > 0)
            {
                dbFile = await  _storageService.StoreAsync(file, 1, null);
            }

			if(dbFile == null)
				return BadRequest();

            return CreatedAtAction(nameof(UploadFile), new { file=dbFile});
        }
	}
}


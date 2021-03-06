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
	[Route("api/Show/File")]
	[ApiController]
    public class ShowFileController : ShowController
    {
		// Only allow 500mb for unregister user
		private const long MaxFileSize = 536870912L;

			public ShowFileController(CSDbContext context, StorageService storageService) : base(context, storageService){
   		}

			[HttpGet("search")]
			public async Task<IActionResult> GetFileBy(string type, string value)
			{
				if(String.IsNullOrEmpty(type) || String.IsNullOrEmpty(value))
					return BadRequest();

				ICollection<LtlFile> files = new List<LtlFile>();
				ICollection<LtlFile> publicFiles = await _storageService.GetFilesByUserIdAsync(1);

			switch(type.ToLower())
			{
				case "name":
					files = _storageService.SearchFilesByName(value,publicFiles);
					break;
				case "id":
					LtlFile file = await _storageService.SearchFileByUniqueIdAsync(value);
					if(file != null)
						files.Add(file);
					break;
				case "type":
					files =_storageService.SearchFileByMimetype(value, publicFiles);
					break;
				default:
					break;
			}

			return Ok(files);
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
		public async Task<IActionResult> GetAllFiles(int? number)
		{
			ICollection<LtlFile>publicFiles = await _storageService.GetFilesByUserIdAsync(1);
			ICollection<LtlFile> files = new List<LtlFile>();
			int total = publicFiles.Count();

			if(number == null)
				files = publicFiles;
			else
				files = publicFiles
					.OrderByDescending(f => f.CreatedAt)
					.Take((int)number).ToList();

			return Ok(new { files, total});
		}

		[HttpGet("{uniqueId}")]
		public async Task<IActionResult> GetFileByUniqueId(string uniqueId)
		{
			LtlFile file = await _storageService.SearchFileByUniqueIdAsync(uniqueId);

			if(file == null)
				return NotFound();

			return Ok(new {msg="success", file});
		}

		// Large file ref: https://stackoverflow.com/questions/62502286/uploading-and-downloading-large-files-in-asp-net-core-3-1
		[RequestSizeLimit(MaxFileSize)]
		[RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
						await _storageService.DeleteAllUselessFiles();

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


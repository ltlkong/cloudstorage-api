using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_cloudstorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly StorageService _storageService;
        public FileController(CSDbContext context, StorageService storageService) : base(context)
        {
            _storageService = storageService;
        }
		[HttpGet("{id}")]
		public async Task<IActionResult> GetBy(int id)
		{
			bool isExitsInUserFiles = await _storageService.CheckIsUserFileAsync(id, GetCurrentUser().Id);

			if(!isExitsInUserFiles)
				return NotFound();
			
			LtlFile file = await _storageService.GetFileByIdAsync(id);

			return Ok(file);
		}
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ICollection<LtlFile> files = await _storageService.GetFilesByUserIdAsync(GetCurrentUser().Id);

            return Ok(files);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm]int? directoryId)
        {
            try
            {
				if(file == null)
					return BadRequest(new {msg="Empty file"});

                long size = file.Length;

                if (file.Length > 0)
                    await _storageService.StoreAsync(file, GetCurrentUser().Id, directoryId);

                return CreatedAtAction(nameof(Upload), new { size, fileName = file.FileName });
            }
			catch
            {
                return BadRequest();
            }    
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [Bind("Name","DirectoryId")]LtlFile file)
        {
			int userId = GetCurrentUser().Id;
			bool isExitsInUserFiles = await _storageService.CheckIsUserFileAsync(id,userId);
			bool isExitsInUserDirs = await _storageService.CheckIsUserDirectoryAsync(file.DirectoryId, userId);

			if(!isExitsInUserFiles) return NotFound();
			if(!isExitsInUserDirs) return BadRequest();

			LtlFile fileToUpdate = await _storageService.GetFileByIdAsync(id);
			fileToUpdate.Name = file.Name;
			fileToUpdate.DirectoryId = file.DirectoryId;
			bool isSuccess = await _storageService.UpdateFileAsync(fileToUpdate);

			if(!isSuccess) return BadRequest();

			return Ok(new {msg="Updated", file});
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
			bool isSuccess = await _storageService.SoftDeleteFileByIdAsync(id);

			if(!isSuccess) 
				return BadRequest();

			return Ok(new {msg="file deleted"});
        }
    }
}

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
		public async Task<IActionResult> GetFileBy(int id)
		{
			LtlFile file = await _storageService.GetFileByIdAsync(id);
			ICollection<LtlFile> files = await _storageService.GetFilesByUserIdAsync(GetCurrentUser().Id);

			LtlFile isExitsInUserFiles = files.FirstOrDefault(f => f.Id == file.Id);
			if(isExitsInUserFiles == null)
				return NotFound();

			return Ok(file);
		}
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            ICollection<LtlFile> files = await _storageService.GetFilesByUserIdAsync(GetCurrentUser().Id);

            return Ok(files);
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm]int? directoryId)
        {
            try
            {
                long size = file.Length;

                if (file.Length > 0)
                {
                    await _storageService.StoreAsync(file, GetCurrentUser().Id, directoryId);
                }

                return CreatedAtAction(nameof(UploadFile), new { size, fileName = file.FileName });
            }catch
            {
                return BadRequest();
            }    
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [Bind("Name","DirectoryId")]LtlFile file)
        {
			if(!await _storageService.CheckIsUserFileAsync(id, GetCurrentUser().Id))
				return BadRequest();

			bool isSuccess = await _storageService.UpdateFileAsync(file);
			if(!isSuccess) 
				return BadRequest();

			return Ok(new {msg="updated", file});
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

using ltl_cloudstorage.Models;
using ltl_cloudstorage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ltl_cloudstorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : BaseController
    {
        private readonly StorageService _storageService;
        public DirectoryController(CSDbContext context, StorageService storageService) : base(context)
        {
            _storageService = storageService;
        }

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			ICollection<LtlDirectory> directories = await _storageService.GetDirectoriesByUserIdAsync(GetCurrentUser().Id);

			return Ok(directories);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBy(int id)
		{
			bool isExitsInUserDirs = await _storageService.CheckIsUserDirectoryAsync(id, GetCurrentUser().Id);

			if(!isExitsInUserDirs) return NotFound();

			LtlDirectory directory = await _context.LtlDirectories.FindAsync(id);

			return Ok(new {directory});
		}
		[HttpPost]
		public async Task<IActionResult> Create([Bind("Name","ParentDirId")]LtlDirectory directory)
		{
		
			bool isSuccess = await _storageService.CreateDirectoryAsync(directory.Name, GetCurrentUser().Id, directory.ParentDirId);

			if(!isSuccess)
				return BadRequest();

			return CreatedAtAction(nameof(Create), new { directoryName=directory.Name});
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(string name, int id)
		{
			LtlDirectory directory = await _context.LtlDirectories.FindAsync(id);
			directory.Name=name;

			await _context.SaveChangesAsync();

			return Ok(new {msg="Updated", directory});
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			bool isUserDir = await _storageService.CheckIsUserDirectoryAsync(id, GetCurrentUser().Id);

			if(!isUserDir)
				return NotFound();

			try
			{
				await _storageService.DeleteDirectoryByIdAsync(id);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(new {msg="Deleted successfully"});
			
			

		}
		

    }
}

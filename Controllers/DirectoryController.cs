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
		public async Task<IActionResult> GetAllDirectories()
		{
			ICollection<LtlDirectory> directories = await _storageService.GetDirectoriesByUserIdAsync(GetCurrentUser().Id);

			return Ok(directories);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDirectoryById(int id)
		{
			ICollection<LtlDirectory> directories = await _storageService.GetDirectoriesByUserIdAsync(GetCurrentUser().Id);
			LtlDirectory directory = directories.FirstOrDefault(dir => dir.Id == id);


			if(directory == null)
				return NotFound();

			List<LtlDirectory> subDirectories = new List<LtlDirectory>();
			await _storageService.GetSubDirectoriesByIdAsync(id, subDirectories);

			return Ok(new {directory, subDirectories});
		}
		[HttpPost]
		public async Task<IActionResult> CreateDirectory([Bind("Name","ParentDirId")]LtlDirectory directory)
		{
		
			bool isSuccess = await _storageService.CreateDirectoryAsync(directory.Name, GetCurrentUser().Id, directory.ParentDirId);

			if(!isSuccess)
				return BadRequest();

			return CreatedAtAction(nameof(CreateDirectory), new { directoryName=directory.Name});
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDirectory(string name, int id)
		{
			LtlDirectory directory = await _context.LtlDirectories.FindAsync(id);
			directory.Name=name;

			await _context.SaveChangesAsync();

			return Ok(new {msg="Updated", directory});
		}

    }
}

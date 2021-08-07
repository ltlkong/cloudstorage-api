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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_cloudstorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : BaseController
    {
        private readonly StorageService _storageService;
        public StorageController(CSDbContext context, StorageService storageService) : base(context)
        {
            _storageService = storageService;
        }

        //Test
        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<IActionResult> Get(string name)
        {
            LtlFile file = (await _storageService.SearchFilesByNameAsync(name)).First();
            Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);
            string mimeType = _storageService.GetFileType(file.Name);

            Response.Headers.Add("Access-Control-Expose-Headers", "content-disposition");

            return File(fileBytes, mimeType, file.Name);
        }

        [AllowAnonymous]
        // Test
        [HttpPost("test")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;

            if (file.Length > 0)
            {
                await _storageService.StoreAsync(file, 1, 1);
            }

            return CreatedAtAction("PostFile", new { size, fileName = file.FileName });
        }

        [Authorize(Roles ="Admin")]
        // GET: api/<StorageController>
        [HttpGet("admin")]
        public async Task<IActionResult> Get(int fileid)
        {
            LtlFile file = await _storageService.GetFileByIdAsync(fileid);
            Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);
            string mimeType = _storageService.GetFileType(file.Name);

            Response.Headers.Add("Access-Control-Expose-Headers", "content-disposition");

            return File(fileBytes, mimeType, file.Name);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilesFromCurrentUser()
        {
            List<LtlFile> files = await _storageService.GetFilesByUserIdAsync(GetCurrentUser().Id);

            return Ok(files);
        }

        [HttpPost]
        public async Task<IActionResult> PostFile(IFormFile file, [FromForm]int? directoryId)
        {
            try
            {
                long size = file.Length;

                if (file.Length > 0)
                {
                    await _storageService.StoreAsync(file, GetCurrentUser().Id, directoryId);
                }

                return CreatedAtAction("PostFile", new { size, fileName = file.FileName });
            }catch
            {
                return BadRequest();
            }    
        }

        // PUT api/<StorageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StorageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

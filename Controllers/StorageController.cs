using ltl_cloudstorage.Models;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ltl_cloudstorage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StorageController : BaseController
    {
        private readonly StorageService _storageService;
        public StorageController(CSDbContext context, StorageService storageService) : base(context)
        {
            _storageService = storageService;
        }

        // GET: api/<StorageController>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            LtlFile file = await _storageService.GetFileByIdAsync(id);
            Byte[] fileBytes = await _storageService.GetFileBytesAsync(file.Path);
            string mimeType = _storageService.GetFileType(file.Name);

            return File(fileBytes, mimeType);
        }

        [HttpPost]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            long size = file.Length;
            bool isSuccess = false;

            if (file.Length > 0)
            {
                isSuccess = await _storageService.StoreAsync(file);
            }

            if (isSuccess)
                return CreatedAtAction("PostFile", new { size, fileName = file.FileName });
            else
                return BadRequest();
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

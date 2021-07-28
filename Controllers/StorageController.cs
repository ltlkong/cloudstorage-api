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
    public class StorageController : ControllerBase
    {
        // GET: api/<StorageController>
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            
            Byte[] b = await System.IO.File.ReadAllBytesAsync(Directory.GetCurrentDirectory() + "/Storage/Images/"+name);

            return File(b, "image/jpg");
        }

        // GET api/<StorageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StorageController>
        [HttpPost]
        public async Task<IActionResult> PostFile(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            string filePath = null;
            List<string> types = new List<string>();


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filePath = Directory.GetCurrentDirectory() + "/Storage/Images/"+ formFile.FileName;

                    types.Add(formFile.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath, types });
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

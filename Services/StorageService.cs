using ltl_cloudstorage.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Services
{
    public class StorageService : BaseService
    {
        public StorageService(CSDbContext context) : base(context)
        {
        }

        public async Task<bool> Store(IFormFile file)
        {
            try
            {
                string filePath = System.IO.Directory.GetCurrentDirectory() + "/Storage/Images/" + System.IO.Path.GetRandomFileName();
                string actualFileName = file.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                await CreateDbDocumentInstanceAsync(actualFileName,filePath);

                return true;
            }
            catch
            {
                return false;
            }
 
        }

        #region helpers
        private string GenerateGuid()
        {
            string guid = Guid.NewGuid().ToString();

            return guid;
        }

        private async Task CreateDbDocumentInstanceAsync(string fileName, string filePath)
        {
            string guid = GenerateGuid();

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            Document document = new Document(guid, fileInfo.Name, fileInfo.Extension, filePath);

            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}

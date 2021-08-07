using ltl_cloudstorage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ltl_cloudstorage.Services
{
    public partial class StorageService : BaseService
    {
        private readonly string _contextStoragePath = System.IO.Directory.GetCurrentDirectory() + "/Storage";
        public StorageService(CSDbContext context) : base(context)
        {
        }

        public async Task<bool> StoreAsync(IFormFile file)
        {
            try
            {
                string filePath = "/" + System.IO.Path.GetRandomFileName();
                string actualFileName = file.FileName;

                using (var stream = System.IO.File.Create(_contextStoragePath + filePath))
                {
                    await file.CopyToAsync(stream);
                }

                await CreateDbDocumentInstanceAsync(actualFileName, filePath, file.Length);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LtlFile> GetFileByIdAsync(int id)
        {
            LtlFile file = await _context.LtlFiles.FindAsync(id);

            return file;
        }

        public async Task<LtlFile> GetFileByUniqueIdAsync(string uniqueId)
        {
            LtlFile file = await _context.LtlFiles.FirstAsync(f => f.UniqueId.Equals(uniqueId));

            return file;
        }
        public async Task<List<LtlFile>> GetFilesByNameAsync(string name)
        {
            List<LtlFile> files = await _context.LtlFiles
                .Where(f => f.Name.ToLower().Contains(name.ToLower())).ToListAsync();

            return files;
        }

        public async Task<List<LtlFile>> GetFilesByDirectoryIdAsync(int id)
        {
            List<LtlFile> files = await _context.LtlFiles
                .Where(f => f.DirectoryId == id).ToListAsync();

            return files;
        }

        public string GetFileType(string fileName)
        {
            string mimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName);

            return mimeType;
        }

        public async Task<Byte[]> GetFileBytesAsync(string filePath)
        {
            Byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(FullPath(filePath));

            return fileBytes;
        }

        public string FullPath(string filePath)
        {
            return _contextStoragePath + filePath;
        }

        #region helpers
        private string GenerateGuid()
        {
            string guid = Guid.NewGuid().ToString();

            return guid;
        }

        private async Task CreateDbDocumentInstanceAsync(string fileName, string filePath, long size)
        {
            string guid = GenerateGuid();

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            LtlFile file = new LtlFile(guid, fileInfo.Name, fileInfo.Extension, filePath, size);

            await _context.LtlFiles.AddAsync(file);
            await _context.SaveChangesAsync();
        }
        #endregion
    }

    public partial class StorageService
    {
        public async Task<bool> CreateDirectoryAsync(string name)
        {
            try
            {
                LtlDirectory directory = new LtlDirectory(GenerateGuid(), name);

                await _context.LtlDirectories.AddAsync(directory);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

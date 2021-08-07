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

        public async Task StoreAsync(IFormFile file, int UserId, int? directoryId)
        {
            string filePath = "/" + System.IO.Path.GetRandomFileName();
            string actualFileName = file.FileName;

            using (var stream = System.IO.File.Create(_contextStoragePath + filePath))
            {
                await file.CopyToAsync(stream);
            }

            if(directoryId == null)
            {
                LtlDirectory defaultDirectory = await GetDefaultDirectoryByUserIdAsync(UserId);
                directoryId = defaultDirectory.Id;
            }
                
            await CreateDbFileInstanceAsync(actualFileName, filePath, file.Length, (int)directoryId);
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
        public async Task<List<LtlFile>> SearchFilesByNameAsync(string name)
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
        public async Task<List<LtlFile>> GetFilesByUserIdAsync(int id)
        {
            List<LtlDirectory> directories = await GetDirectoriesByUserIdAsync(id);
            List<LtlFile> files = new List<LtlFile>();

            foreach(LtlDirectory directory in directories)
            {
                files.AddRange(await GetFilesByDirectoryIdAsync(directory.Id));
            }

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

        private async Task CreateDbFileInstanceAsync(string fileName, string filePath, long size, int directoryId)
        {
            string guid = GenerateGuid();

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            LtlFile file = new LtlFile(guid, fileInfo.Name, fileInfo.Extension, filePath, size, directoryId);

            await _context.LtlFiles.AddAsync(file);
            await _context.SaveChangesAsync();
        }
        #endregion
    }

    public partial class StorageService
    {
        public async Task<bool> CreateDirectoryAsync(string name, int userId)
        {
            LtlDirectory directory = await GetDirectoryByNameAsync(name);

            if (directory != null)
                return false;

            directory = new LtlDirectory(GenerateGuid(), name, userId);

            await _context.LtlDirectories.AddAsync(directory);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<LtlDirectory>> GetDirectoriesByUserIdAsync(int id)
        {
            List<LtlDirectory> directories = await _context.LtlDirectories
                .Where(d => d.UserInfoId == id).ToListAsync();

            return directories;
        }

        public async Task<List<LtlDirectory>> SearchDirectoryByNameAsync(string name)
        {
            List<LtlDirectory> directories = await _context.LtlDirectories
                .Where(d => d.Name.ToLower().Equals(name.ToLower())).ToListAsync();

            return directories;
        }

        public async Task<LtlDirectory> GetDirectoryByNameAsync(string name)
        {
            LtlDirectory directory = await _context.LtlDirectories.FirstOrDefaultAsync(d => d.Name.Equals(name));

            return directory;
        }

        public LtlDirectory GetDirectoryByName(string name, List<LtlDirectory> directories)
        {
            LtlDirectory directory = directories.FirstOrDefault(d => d.Name.Equals(name));

            return directory;
        }

        public async Task<LtlDirectory> GetDefaultDirectoryByUserIdAsync(int id)
        {
            List<LtlDirectory> directories = await GetDirectoriesByUserIdAsync(id);
            LtlDirectory defaultDirectory = GetDirectoryByName("Default", directories);

            if(defaultDirectory == null)
            {
                await CreateDirectoryAsync("Default", id);
            }

            defaultDirectory = await _context.LtlDirectories
                .FirstAsync(d => d.Name.Equals("Default"));

            return defaultDirectory;
        }
    }
}

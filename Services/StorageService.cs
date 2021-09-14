﻿using ltl_cloudstorage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
            string filePath = "/" + Path.GetRandomFileName();
            string actualFileName = file.FileName;

            if(directoryId == null)
            {
                LtlDirectory defaultDirectory = await GetDefaultDirectoryByUserIdAsync(UserId);
                directoryId = defaultDirectory.Id;
            }
                
            await CreateDbFileInstanceAsync(actualFileName, filePath, file.Length, (int)directoryId);

            using (var stream = System.IO.File.Create(_contextStoragePath + filePath))
            {
                await file.CopyToAsync(stream);
            }
        }
        public async Task<LtlFile> GetFileByIdAsync(int id)
        {
            LtlFile file = await _context.LtlFiles.FindAsync(id);

			if(file != null && !file.isDeleted )
				return file;

			return null;
        }
        public async Task<ICollection<LtlFile>> SearchFilesByNameAsync(string name)
        {
            List<LtlFile> files = await _context.LtlFiles
                .Where(f => f.Name.ToLower().Contains(name.ToLower()) && !f.isDeleted).ToListAsync();

            return files;
        }

		public async Task<LtlFile> SearchFileByUniqueIdAsync(string uniqueId)
		{
			LtlFile file = await _context.LtlFiles.SingleOrDefaultAsync(f => f.UniqueId.Equals(uniqueId) && !f.isDeleted);

			return file;
		}

		public async Task<bool> CheckIsUserFileAsync(int fileId, int userId)
		{
			ICollection<LtlFile> files = await GetFilesByUserIdAsync(userId);
			
			if(files.SingleOrDefault(f => f.Id == fileId) != null)
				return true;

			return false;
		}


        public async Task<ICollection<LtlFile>> GetFilesByDirectoryIdAsync(int id)
        {
            List<LtlFile> files = await _context.LtlFiles
                .Where(f => f.DirectoryId == id && !f.isDeleted).ToListAsync();

            return files;
        }
        public async Task<ICollection<LtlFile>> GetFilesByUserIdAsync(int id)
        {
            List<LtlDirectory> directories = (await GetDirectoriesByUserIdAsync(id)).ToList();
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
		public async Task<bool>  UpdateFileAsync(LtlFile file)
		{
			try
			{
				_context.Entry(file).State = EntityState.Modified;

				await _context.SaveChangesAsync();
				return true;
			}catch{
				return false;
			}

		}
		public async Task<bool> SoftDeleteFileByIdAsync(int id)
		{
			bool isSuccess = false;

			LtlFile file = await GetFileByIdAsync(id);

			if(file == null)
				return isSuccess;

			file.isDeleted = true;
			file.DirectoryId = 1;
			file.UniqueId = "deleted"+file.Id;
			isSuccess = await UpdateFileAsync(file);

			return isSuccess;
		}
		
		// Completely remove a file
		public async Task<bool> HardDeleteFileByIdAsync(int id)
		{
			bool isSuccess = false;

			LtlFile file = await GetFileByIdAsync(id);

			if(file == null)
				return isSuccess;

			File.Delete(FullPath(file.Path));
			_context.Entry(file).State = EntityState.Deleted;

			await _context.SaveChangesAsync();

			return isSuccess;
		}

        #region helpers
        private string GenerateGuid()
        {
            string guid = Guid.NewGuid().ToString();

            return guid;
        }
        private async Task CreateDbFileInstanceAsync(string fileName, string filePath, long size, int directoryId)
        {
            LtlFile file = new LtlFile(GenerateGuid(), fileName, GetFileType(fileName), filePath, size, directoryId);

            await _context.LtlFiles.AddAsync(file);
            await _context.SaveChangesAsync();
        }
        public string FullPath(string filePath)
        {
            return _contextStoragePath + filePath;
        }
        #endregion
    }

    public partial class StorageService
    {
        public async Task<bool> CreateDirectoryAsync(string name, int userId, int? parentDirId)
        {
            ICollection<LtlDirectory> directories = await GetDirectoryByNameAsync(name);
			// Get the same layer dir avoid duplicate names
			LtlDirectory sameLayerDir= directories
				.SingleOrDefault(dir => dir.Name.Equals(name) && dir.UserInfoId == userId && dir.ParentDirId == parentDirId);

            if (sameLayerDir != null)
                return false;

            LtlDirectory directory = new LtlDirectory(GenerateGuid(), name, userId, parentDirId);

            await _context.LtlDirectories.AddAsync(directory);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<ICollection<LtlDirectory>> GetDirectoriesByUserIdAsync(int id)
        {
            List<LtlDirectory> directories = await _context.LtlDirectories
                .Where(d => d.UserInfoId == id).ToListAsync();

            return directories;
        }
        public async Task<ICollection<LtlDirectory>> SearchDirectoryByNameAsync(string name)
        {
            List<LtlDirectory> directories = await _context.LtlDirectories
                .Where(d => d.Name.ToLower().Equals(name.ToLower())).ToListAsync();

            return directories;
        }
        public async Task<ICollection<LtlDirectory>> GetDirectoryByNameAsync(string name)
        {
            List<LtlDirectory> directories = await _context.LtlDirectories.Where(d => d.Name.Equals(name)).ToListAsync();

            return directories;
        }
        public LtlDirectory GetDirectoryByName(string name, ICollection<LtlDirectory> directories)
        {
            LtlDirectory directory = directories.FirstOrDefault(d => d.Name.Equals(name));

            return directory;
        }
        public async Task<LtlDirectory> GetDefaultDirectoryByUserIdAsync(int id)
        {
            ICollection<LtlDirectory> directories = await GetDirectoriesByUserIdAsync(id);
            LtlDirectory defaultDirectory = GetDirectoryByName("Default", directories);

            if(defaultDirectory == null)
            {
                bool isSuccess = await CreateDirectoryAsync("Default", id, null);

				if(isSuccess)
					defaultDirectory = GetDirectoryByName("Default", await GetDirectoriesByUserIdAsync(id));
            }
			else
            	defaultDirectory = GetDirectoryByName("Default", directories);

            return defaultDirectory;
        }

		public async Task GetSubDirectoriesByIdAsync(int id, List<LtlDirectory> subDirectories)
		{
			LtlDirectory directory = await _context.LtlDirectories
				.Include(dir => dir.ChildDirs)
				.FirstOrDefaultAsync(dir => dir.Id==id);
			subDirectories.AddRange(directory.ChildDirs);

			if(directory.ChildDirs.Count == 0)
				return;

			foreach(LtlDirectory dir in directory.ChildDirs)
			{
				await GetSubDirectoriesByIdAsync(dir.Id, subDirectories);
			}
		}

		public async Task<bool> CheckIsUserDirectoryAsync(int directoryId, int userId)
		{
			ICollection<LtlDirectory> directories = await GetDirectoriesByUserIdAsync(userId);
			
			if(directories.SingleOrDefault(dir => dir.Id == directoryId) != null)
				return true;

			return false;
		}
    }
}

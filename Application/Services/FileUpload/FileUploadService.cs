using Application.FileUpload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.FileUpload
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _storagePath;

        public FileUploadService(IConfiguration configuration)
        {
            _storagePath = configuration["FileUpload:StoragePath"] ?? "wwwroot/uploads";
        }

        public async Task<bool> RemoveFile(string path)
        {
            var fullPath = Path.Combine(_storagePath, path);
            if (!File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or not provided.");
            }

            // ✅ تصحیح: file.FileName نه file.Name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fullPath;
        }
    }
}
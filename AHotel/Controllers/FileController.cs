using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _storagePath;

        public FileController(IConfiguration configuration)
        {
            _storagePath = configuration["FileUpload:StoragePath"] ?? "wwwroot/uploads";

            if (!System.IO.Directory.Exists(_storagePath))
            {
                System.IO.Directory.CreateDirectory(_storagePath);
            }
        }

        [HttpGet("GetFile")]
        public IActionResult GetFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return NotFound("Filename is required.");

            var fullPath = System.IO.Path.Combine(_storagePath, filename);

            if (!System.IO.File.Exists(fullPath))
                return NotFound($"File not found: {filename}");

            var contentType = GetContentType(filename);
            return PhysicalFile(fullPath, contentType);
        }

        private string GetContentType(string filename)
        {
            var extension = System.IO.Path.GetExtension(filename).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }
    }
}
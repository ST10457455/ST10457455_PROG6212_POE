// Services/LocalFileService.cs
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ClaimSystem.Services
{
    public class LocalFileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowed = new[] { ".pdf", ".docx", ".xlsx" };
        public long MaxFileSizeInBytes => 5 * 1024 * 1024; // 5 MB

        public LocalFileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool IsValidExtension(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return _allowed.Contains(ext);
        }

        public async Task<(bool Success, string? StoredFileName, string? Path, string? ErrorMessage)> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, null, null, "No file provided.");

            if (file.Length > MaxFileSizeInBytes)
                return (false, null, null, $"File too large. Max allowed is {MaxFileSizeInBytes} bytes.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowed.Contains(ext))
                return (false, null, null, "Invalid file type.");

            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var storedName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(uploads, storedName);

            try
            {
                using var stream = System.IO.File.Create(fullPath);
                await file.CopyToAsync(stream);
                return (true, storedName, $"/uploads/{storedName}", null);
            }
            catch (Exception ex)
            {
                return (false, null, null, $"Failed to save file: {ex.Message}");
            }
        }
    }
}

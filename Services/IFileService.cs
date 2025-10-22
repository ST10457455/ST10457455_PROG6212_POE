// Services/IFileService.cs
using Microsoft.AspNetCore.Http;

namespace ClaimSystem.Services
{
    public interface IFileService
    {
        Task<(bool Success, string? StoredFileName, string? Path, string? ErrorMessage)> SaveFileAsync(IFormFile file);
        bool IsValidExtension(string fileName);
        long MaxFileSizeInBytes { get; }
    }
}

using Booking.Application.Services.FileUpload;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<FileService> _logger;

    public FileService(IWebHostEnvironment webHostEnvironment, ILogger<FileService> logger)
    {
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }

    public async Task<string> UploadImage(string location, IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError("File is null or empty.");
                return "NoImage";
            }

            // Ensure the directory path is correct
            var webRootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(webRootPath))
            {
                _logger.LogError("WebRootPath is null or empty.");
                return "FailedToUploadImage";
            }

            var path = Path.Combine(webRootPath, location ?? string.Empty);

            // Create directory if it doesn't exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Generate unique file name
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(path, fileName);

            // Copy file to server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            // Return relative file path
            return $"/{location}/{fileName}";
        }
        catch (Exception ex)
        {
            // Log error and handle exceptions
            _logger.LogError(ex, "Failed to upload image.");
            return "FailedToUploadImage";
        }
    }

}

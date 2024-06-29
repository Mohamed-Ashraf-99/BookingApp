
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Services.FileUpload;

public interface IFileService
{
    Task<string> UploadImage(string Location, IFormFile file);
}

using Microsoft.AspNetCore.Http;

namespace Services.Services;

public interface IImageService
{
    public Task<string> SaveImage( IFormFile formFile, Guid userId );
}
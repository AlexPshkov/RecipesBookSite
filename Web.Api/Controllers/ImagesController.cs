using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dto.Responses;
using Services.Services;
using Web.Api.Filters;

namespace Web.Api.Controllers;

[Authorize]
[ApiController]
[Route( "api/[controller]" )]
[TypeFilter( typeof( ExceptionsFilter ) )]
public class ImagesController : Controller
{
    private Guid UserId => Guid.Parse( User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value );
    
    private readonly ILogger<ImagesController> _logger;
    private readonly IImageService _imageService;

    public ImagesController( ILogger<ImagesController> logger, IImageService imageService )
    {
        _logger = logger;
        _imageService = imageService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Save( IFormFile formFile )
    {
        Guid userId = UserId;
        _logger.LogInformation( "Saving new image file {Name}. Sender UserID: {UserId}", formFile.Name, userId );
        
        string imagePath = await _imageService.SaveImage( formFile, userId );
        
        _logger.LogInformation( "Success! Image {Name} from UserID: {UserId} successfully saved. Image path: {ImagePath}", formFile.Name, userId, imagePath );
        return Ok( new ImageLoaded
        {
            ImagePath = imagePath
        } );
    }
}
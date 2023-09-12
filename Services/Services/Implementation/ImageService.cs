using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Services.Services.Implementation;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHost;

    public ImageService( IWebHostEnvironment webHost )
    {
        _webHost = webHost;
    }

    public async Task<string> SaveImage( IFormFile formFile, Guid userId )
    {
        string filePath = GetPath( formFile.FileName, userId );

        await using FileStream stream = File.Create( filePath );
        await formFile.CopyToAsync( stream );
        
        return GetNormalPath( filePath );
    }

    private string GetNormalPath( string rootPath )
    {
        return Path.GetRelativePath( Path.Combine( _webHost.WebRootPath, "images" ), rootPath ).Replace( "\\", "/" );
    }

    private string GetPath( string fileName, Guid userId )
    {
        DateTime date = DateTime.Now;
        string dirPath = Path.Combine( _webHost.WebRootPath, "images",
            date.Year.ToString(),
            date.Month.ToString(),
            date.Day.ToString(),
            userId.ToString() );

        if ( !Directory.Exists( dirPath ) )
        {
            Directory.CreateDirectory( dirPath );
        }

        return Path.Combine( dirPath, Path.GetRandomFileName() + Path.GetExtension( fileName ) );
    }
}
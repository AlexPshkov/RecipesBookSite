using Domain.Models;

namespace Services.Services;

public interface ISecurityService
{
    public string HashPassword( string password );
    public bool VerifyPassword( string password, string passwordHash );
    public string GetToken( UserEntity userEntity );
}
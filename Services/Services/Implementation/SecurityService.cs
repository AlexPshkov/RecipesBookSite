using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Domain.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Extensions.Entity;

namespace Services.Services.Implementation;

public class SecurityService : ISecurityService
{
    private readonly AuthOptions _authOptions;

    public SecurityService( IOptions<AuthOptions> authOptions )
    {
        _authOptions = authOptions.Value;
    }

    public string HashPassword( string password )
    {
        string? salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword( password, salt );
    }

    public bool VerifyPassword( string password, string passwordHash )
    {
        return BCrypt.Net.BCrypt.Verify( password, passwordHash );
    }

    public string GetToken( UserEntity userEntity )
    {
        SigningCredentials credentials =
            new SigningCredentials( _authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256 );

        List<Claim> claims = new List<Claim>
        {
            new Claim( JwtRegisteredClaimNames.Name, userEntity.Login ),
            new Claim( JwtRegisteredClaimNames.Sub, userEntity.UserId.ToString() ),
            new Claim( "role", userEntity.Role.ToString() )
        };

        JwtSecurityToken token = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            claims,
            expires: DateTime.Now.AddSeconds( _authOptions.TokenLifetime ),
            signingCredentials: credentials );

        return new JwtSecurityTokenHandler().WriteToken( token );
    }
}
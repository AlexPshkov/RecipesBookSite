using System.Text;
using Domain.Models.Auth;
using Microsoft.IdentityModel.Tokens;

namespace Services.Extensions.Entity;

public static class AuthOptionsExtensions
{
    public static SymmetricSecurityKey GetSymmetricSecurityKey( this AuthOptions authOptions )
    {
        return new SymmetricSecurityKey( Encoding.ASCII.GetBytes( authOptions.Secret ) );
    }
}
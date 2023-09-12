using Domain.Models;
using Services.Dto.Requests;

namespace Services.Extensions.Requests;

public static class RegisterRequestExtensions
{
    public static UserEntity ConvertToUserEntity( this RegisterRequest request )
    {
        return new UserEntity
        {
            UserId = Guid.NewGuid(),
            UserName = request.Name,
            Description = "",
            Login = request.Login.ToLower(),
            Password = BCrypt.Net.BCrypt.HashPassword( request.Password )
        };
    }
}
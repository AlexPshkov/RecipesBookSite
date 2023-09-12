using Domain.Models;
using Services.Dto.Requests;

namespace Services.Extensions.Requests;

public static class ChangeUserDataRequestExtensions
{
    public static UserEntity ConvertToUserEntity( this ChangeUserDataRequest request, Guid userId )
    {
        return new UserEntity
        {
            UserId = userId,
            UserName = request.UserName,
            Login = request.Login.ToLower(),
            Password = string.IsNullOrEmpty( request.Password ) ? "" : BCrypt.Net.BCrypt.HashPassword( request.Password ),
            Description = request.Description,
            Role = Enum.TryParse( request.Role, out Role role ) ? role : Role.User
        };
    }
}
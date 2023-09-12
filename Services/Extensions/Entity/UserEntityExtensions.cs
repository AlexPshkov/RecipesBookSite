using Domain.Exceptions.Implementation;
using Domain.Models;
using Services.Dto;

namespace Services.Extensions.Entity;

public static class UserEntityExtensions
{
    /**
     * <exception cref="NoSuchUserException"></exception>
     */
    public static UserDto ConvertToUserDto( this UserEntity? userEntity )
    {
        if ( userEntity == null )
        {
            throw new NoSuchUserException();
        }

        return new UserDto
        {
            UserName = userEntity.UserName,
            Description = userEntity.Description,
            Login = userEntity.Login,
            Role = userEntity.Role.ToString()
        };
    }

    /**
     * <exception cref="NoSuchUserException"></exception>
     */
    public static UserEntity Combine( this UserEntity? userEntity, UserEntity? newUserEntity )
    {
        if ( userEntity == null || newUserEntity == null )
        {
            throw new NoSuchUserException();
        }

        userEntity.UserName = newUserEntity.UserName;
        userEntity.Description = newUserEntity.Description;
        userEntity.Login = newUserEntity.Login;

        if ( !string.IsNullOrEmpty( newUserEntity.Password ) )
        {
            userEntity.Password = newUserEntity.Password;
        }

        return userEntity;
    }
}
using Domain.Models;

namespace Domain.Exceptions.Implementation;

public class InvalidUserException : AbstractRuntimeException
{
    public InvalidUserException()
    {
    }
    
    public InvalidUserException( string message, UserEntity userEntity ) 
        : base( $"Invalid user {userEntity} Error: {message}" )
    {
    }
}
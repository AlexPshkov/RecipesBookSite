namespace Domain.Exceptions.Implementation;

public class NoSuchUserException : AbstractRuntimeException
{
    public NoSuchUserException()
    {
    }
    
    public NoSuchUserException( Guid userId ) 
        : base( $"No such user with ID: {userId}" )
    {
    }
    
    public NoSuchUserException( string userLogin ) 
        : base( $"No such user with Login: {userLogin}" )
    {
    }
    
    public override int GetHttpStatusCode()
    {
        return 404;
    }
}
namespace Domain.Exceptions.Implementation;

public class InvalidAuthException : AbstractRuntimeException
{
    public InvalidAuthException() 
        : base("Invalid login or password" )
    {
    }
}
namespace Domain.Exceptions.Implementation;

public class NoPermException : AbstractRuntimeException
{
    public NoPermException( string message) 
        : base( $"No permissions. Error: {message}" )
    {
    }
}
namespace Domain.Exceptions.Implementation;

public class NoPermException : AbstractRuntimeException
{
    public NoPermException( string message) 
        : base( $"No permissions. Error: {message}" )
    {
    }
    
    public override int GetHttpStatusCode()
    {
        return 403;
    }
}
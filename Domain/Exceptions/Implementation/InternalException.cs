namespace Domain.Exceptions.Implementation;

public class InternalException : AbstractRuntimeException
{
    public InternalException( string message, Exception innerException ) 
        : base( message, innerException )
    {
    }
}
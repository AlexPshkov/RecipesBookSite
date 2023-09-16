using System.Net;

namespace Domain.Exceptions.Implementation;

public class InternalException : AbstractRuntimeException
{
    public InternalException( string message, Exception innerException ) 
        : base( message, innerException )
    {
    }

    public override int GetHttpStatusCode()
    {
        return 500;
    }
}
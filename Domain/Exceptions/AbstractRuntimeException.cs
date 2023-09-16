using System.Net;

namespace Domain.Exceptions;

public abstract class AbstractRuntimeException : Exception
{

    protected AbstractRuntimeException()
    {
    }

    protected AbstractRuntimeException( string message, Exception inner ) : base( message, inner )
    {
    }

    protected AbstractRuntimeException( string message ) : base( message )
    {
    }

    public abstract int GetHttpStatusCode();
}
﻿namespace Domain.Exceptions.Implementation;

public class InvalidParamException : AbstractRuntimeException
{
    public InvalidParamException( string message, string paramName = "parameter" ) 
        : base( $"Invalid {paramName}: {message}" )
    {
    }
}
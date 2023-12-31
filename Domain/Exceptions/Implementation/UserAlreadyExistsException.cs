﻿namespace Domain.Exceptions.Implementation;

public class UserAlreadyExistsException : AbstractRuntimeException
{
    public UserAlreadyExistsException( string login ) 
        : base( $"User with login = {login} already exists" )
    {
    }
    
    public override int GetHttpStatusCode()
    {
        return 423;
    }
}
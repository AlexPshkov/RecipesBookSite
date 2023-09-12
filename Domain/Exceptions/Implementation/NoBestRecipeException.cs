namespace Domain.Exceptions.Implementation;

public class NoBestRecipeException : AbstractRuntimeException
{
    public NoBestRecipeException() 
        : base( "There is no best recipe" )
    {
    }
}
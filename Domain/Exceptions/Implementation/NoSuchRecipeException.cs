namespace Domain.Exceptions.Implementation;

public class NoSuchRecipeException : AbstractRuntimeException
{
    public NoSuchRecipeException()
    {
    }
    
    public NoSuchRecipeException( int recipeId ) 
        : base( $"No such recipe with ID:{recipeId}" )
    {
    }
    
    public override int GetHttpStatusCode()
    {
        return 404;
    }
}
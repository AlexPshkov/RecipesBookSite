using Domain.Models;

namespace Domain.Exceptions.Implementation;

public class InvalidRecipeException : AbstractRuntimeException
{

    public InvalidRecipeException()
    {
    }
    
    public InvalidRecipeException( string message, RecipeEntity recipeEntity ) 
        : base( $"Invalid recipe {recipeEntity} Error: {message}" )
    {
    }
    
    public override int GetHttpStatusCode()
    {
        return 412;
    }
}
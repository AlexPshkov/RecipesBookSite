using Domain.Exceptions.Implementation;
using Domain.Models;
using Domain.Models.secondary;

namespace Domain.Validators;

public static class RecipesValidators
{
    public static RecipeEntity ValidateRecipe( this RecipeEntity recipeEntity )
    {
        ValidateName( recipeEntity.RecipeName );
        ValidateDescription( recipeEntity.RecipeDescription );
        ValidateServingsAmount( recipeEntity.ServingsAmount );
        ValidateRequiredTime( recipeEntity.RequiredTime );
        ValidateImagePath( recipeEntity.ImagePath );

        recipeEntity.Ingredients.ValidateIngredients();
        recipeEntity.Steps.ValidateSteps();
        recipeEntity.Tags.ValidateTags();

        return recipeEntity;
    }

    public static string ValidateName( string recipeName )
    {
        if ( string.IsNullOrEmpty( recipeName ) )
        {
            throw new InvalidParamException( "RecipeName is empty" );
        }

        if ( recipeName.Length > 150 )
        {
            throw new InvalidParamException( "Recipe name is too big" );
        }

        return recipeName;
    }

    public static string ValidateDescription( string recipeDescription )
    {
        if ( string.IsNullOrEmpty( recipeDescription ) )
        {
            throw new InvalidParamException( "RecipeDescription is empty" );
        }

        if ( recipeDescription.Length > 150 )
        {
            throw new InvalidParamException( "Recipe description is too big" );
        }

        return recipeDescription;
    }

    public static string ValidateServingsAmount( string servingsAmount )
    {
        if ( servingsAmount.Length > 50 )
        {
            throw new InvalidParamException( "Recipe servings amount is too big" );
        }

        return servingsAmount;
    }

    public static string ValidateRequiredTime( string requiredTime )
    {
        if ( requiredTime.Length > 50 )
        {
            throw new InvalidParamException( "Recipe required time is too big" );
        }

        return requiredTime;
    }

    public static string ValidateImagePath( string imagePath )
    {
        if ( imagePath.Length > 550 )
        {
            throw new InvalidParamException( "Recipe image path is too big" );
        }

        return imagePath;
    }

    private static void ValidateIngredients( this IEnumerable<IngredientEntity> ingredientEntities )
    {
        List<IngredientEntity> ingredients = ingredientEntities.Where( entity =>
            !string.IsNullOrEmpty( entity.Title ) &&
            !string.IsNullOrEmpty( entity.Description ) ).ToList();

        foreach ( IngredientEntity? ingredientEntity in ingredients )
        {
            if ( ingredientEntity.Title.Length > 450 )
            {
                throw new InvalidParamException( "Ingredient title is too big" );
            }
            
            if ( ingredientEntity.Description.Length > 950 )
            {
                throw new InvalidParamException( "Ingredient description is too big" );
            }
        }
    }

    private static void ValidateSteps( this IEnumerable<StepEntity> stepEntities )
    {
        List<StepEntity> steps = stepEntities.Where( entity => !string.IsNullOrEmpty( entity.Description ) ).ToList();

        if ( steps.Any( stepEntity => stepEntity.Description.Length > 450 ) )
        {
            throw new InvalidParamException( "Step description is too big" );
        }
    }

    private static void ValidateTags( this IEnumerable<TagEntity> tagEntities )
    {
        List<TagEntity> tags = tagEntities.Where( entity => !string.IsNullOrEmpty( entity.Name ) ).ToList();

        if ( tags.Any( tagEntity => tagEntity.Name.Length > 450 ) )
        {
            throw new InvalidParamException( "Tag name is too big" );
        }
    }
}
using Domain.Exceptions.Implementation;
using Domain.Models;
using Domain.Models.secondary;
using Services.Dto.Recipe;
using Action = Domain.Models.Action;

namespace Services.Extensions.Entity;

public static class RecipesEntityExtensions
{
    /**
     * <exception cref="NoSuchRecipeException"></exception>
     */
    public static RecipeDto ConvertToRecipeDto( this RecipeEntity? recipeEntity, Guid? userId = null )
    {
        if ( recipeEntity == null )
        {
            throw new NoSuchRecipeException();
        }

        return new RecipeDto
        {
            Id = recipeEntity.RecipeId,
            RecipeName = recipeEntity.RecipeName,
            RecipeDescription = recipeEntity.RecipeDescription,
            ImagePath = recipeEntity.ImagePath,
            RequiredTime = recipeEntity.RequiredTime,
            ServingsAmount = recipeEntity.ServingsAmount,
            UserLogin = recipeEntity.User!.Login,
            FavoritesAmount = recipeEntity.Favorites.Count,
            LikesAmount = recipeEntity.Likes.Count,
            IsCreator = userId != null && recipeEntity.User.UserId == userId,
            IsLiked = userId != null && recipeEntity.Likes.Exists( x => x.UserId == userId ),
            IsFavorite = userId != null && recipeEntity.Favorites.Exists( x => x.UserId == userId ),
            Tags = recipeEntity.Tags.ConvertAll( input => input.ConvertToTagDto() ),
            Ingredients = recipeEntity.Ingredients.ConvertAll( input => input.ConvertToIngredientDto() ),
            Steps = recipeEntity.Steps.ConvertAll( input => input.ConvertToStepDto() )
        };
    }

    /**
     * <exception cref="NoSuchRecipeException"></exception>
     */
    public static RecipeEntity Combine( this RecipeEntity? recipeEntity, RecipeEntity? newRecipeEntity )
    {
        if ( recipeEntity == null || newRecipeEntity == null )
        {
            throw new NoSuchRecipeException();
        }

        recipeEntity.RecipeName = newRecipeEntity.RecipeName;
        recipeEntity.RecipeDescription = newRecipeEntity.RecipeDescription;
        recipeEntity.Ingredients = newRecipeEntity.Ingredients;
        recipeEntity.Steps = newRecipeEntity.Steps;
        recipeEntity.Tags = newRecipeEntity.Tags;
        recipeEntity.ImagePath = newRecipeEntity.ImagePath;
        recipeEntity.RequiredTime = newRecipeEntity.RequiredTime;
        recipeEntity.ServingsAmount = newRecipeEntity.ServingsAmount;
        return recipeEntity;
    }

    /**
     * <exception cref="NoSuchRecipeException"></exception>
     */
    public static RecipeActionEntity ConvertToRecipeActionEntity( this RecipeEntity? recipeEntity, Action action )
    {
        if ( recipeEntity == null )
        {
            throw new NoSuchRecipeException();
        }

        return new RecipeActionEntity
        {
            ActionId = 0,
            Action = action,
            RecipeId = recipeEntity.RecipeId,
            Recipe = recipeEntity,
            User = recipeEntity.User,
            UserId = recipeEntity.UserId,
            ActionDay = DateTimeOffset.Now.DayOfYear,
        };
    }

    public static IngredientDto ConvertToIngredientDto( this IngredientEntity? ingredientEntity )
    {
        if ( ingredientEntity == null )
        {
            return new IngredientDto();
        }

        return new IngredientDto
        {
            Id = ingredientEntity.IngredientId,
            Title = ingredientEntity.Title,
            Description = ingredientEntity.Description,
        };
    }

    public static StepDto ConvertToStepDto( this StepEntity? stepEntity )
    {
        if ( stepEntity == null )
        {
            return new StepDto();
        }

        return new StepDto
        {
            Id = stepEntity.StepId,
            Description = stepEntity.Description
        };
    }

    public static TagDto ConvertToTagDto( this TagEntity? tagEntity )
    {
        if ( tagEntity == null )
        {
            return new TagDto();
        }

        return new TagDto
        {
            Id = tagEntity.TagId,
            TagName = tagEntity.Name
        };
    }
}
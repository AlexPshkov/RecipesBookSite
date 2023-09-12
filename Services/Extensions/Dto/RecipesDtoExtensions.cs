using Domain.Models;
using Domain.Models.secondary;
using Services.Dto.Recipe;

namespace Services.Extensions.Dto;

public static class RecipesDtoExtensions
{
    public static RecipeEntity ConvertToRecipeEntity( this RecipeDto dto, Guid userId )
    {
        return new RecipeEntity
        {
            UserId = userId,
            RecipeId = dto.Id,
            RecipeName = dto.RecipeName,
            RecipeDescription = dto.RecipeDescription,
            ImagePath = dto.ImagePath,
            RequiredTime = dto.RequiredTime,
            ServingsAmount = dto.ServingsAmount,
            Favorites = new List<FavoriteEntity>(),
            Likes = new List<LikeEntity>(),
            Tags = dto.Tags.ConvertAll( input => input.ConvertToTagEntity() ),
            Ingredients = dto.Ingredients.ConvertAll( input => input.ConvertToIngredientEntity() ),
            Steps = dto.Steps.ConvertAll( input => input.ConvertToStepEntity() )
        };
    }

    private static TagEntity ConvertToTagEntity( this TagDto dto )
    {
        return new TagEntity
        {
            TagId = dto.Id,
            Name = dto.TagName
        };
    }

    private static LikeEntity ConvertToLikeEntity( this LikeDto dto )
    {
        return new LikeEntity
        {
            LikeId = dto.Id,
            UserId = Guid.Parse( dto.UserId ),
            RecipeId = dto.RecipeId
        };
    }

    private static FavoriteEntity ConvertToFavoriteEntity( this FavoriteDto dto )
    {
        return new FavoriteEntity
        {
            FavoriteId = dto.Id,
            UserId = Guid.Parse( dto.UserId ),
            RecipeId = dto.RecipeId
        };
    }

    private static IngredientEntity ConvertToIngredientEntity( this IngredientDto dto )
    {
        return new IngredientEntity
        {
            IngredientId = dto.Id,
            Description = dto.Description,
            Title = dto.Title
        };
    }

    private static StepEntity ConvertToStepEntity( this StepDto dto )
    {
        return new StepEntity
        {
            StepId = dto.Id,
            Description = dto.Description
        };
    }
}
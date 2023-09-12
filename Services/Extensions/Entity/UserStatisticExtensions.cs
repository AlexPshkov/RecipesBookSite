using Domain.Exceptions.Implementation;
using Domain.Models;
using Services.Dto;

namespace Services.Extensions.Entity;

public static class UserStatisticExtensions
{
    /**
     * <exception cref="NoSuchUserException"></exception>
     */
    public static UserStatisticDto ConvertToUserStatisticDto( this UserStatisticEntity? statisticEntity )
    {
        if ( statisticEntity == null )
        {
            throw new NoSuchUserException();
        }
        
        return new UserStatisticDto()
        {
            CreatedRecipesAmount = statisticEntity.CreatedRecipesAmount,
            LikedRecipesAmount = statisticEntity.CreatedRecipesLikesAmount,
            FavoritesRecipesAmount = statisticEntity.CreatedRecipesFavoritesAmount
        };
    } 
}
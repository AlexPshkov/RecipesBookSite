using Domain.Models;

namespace Services.Services;

public interface IUserService
{
    public Task<UserEntity?> GetUserById( Guid id );
    public Task<UserEntity?> GetUserByLogin( string login );
    public Task<UserEntity> Save( UserEntity userEntity );

    public Task<UserStatisticEntity> GetUserStatistic( Guid userId );
    public Task<List<RecipeEntity>> GetFavorites( Guid userId, int start, int end );
    public Task<List<RecipeEntity>> GetCreatedRecipes( Guid userId, int start, int end );
}
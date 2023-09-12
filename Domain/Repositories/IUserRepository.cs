using Domain.Models;

namespace Domain.Repositories;

public interface IUserRepository : IEntityRepository<UserEntity>
{
    public Task<UserEntity?> GetById( Guid id );
    public Task<UserEntity?> GetByLogin( string login );

    public Task<UserStatisticEntity> GetUserStatistic( Guid userId );
    public Task<List<RecipeEntity>> GetCreatedRecipes( Guid userId, int start, int end );
    public Task<List<RecipeEntity>> GetFavorites( Guid userId, int start, int end );
}
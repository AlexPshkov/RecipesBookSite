using Domain.Models;
using Action = Domain.Models.Action;

namespace Domain.Repositories;

public interface IRecipeRepository : IEntityRepository<RecipeEntity>
{
    public Task<List<RecipeEntity>> GetAll( int start, int end );

    public Task<List<RecipeEntity>> GetRecipesBySearchQuery( string searchQuery, int start, int end );
    public Task<RecipeEntity?> GetBestRecipe( Action action );
    public Task<RecipeEntity?> GetById( int id );
}
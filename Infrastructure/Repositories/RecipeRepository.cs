using Domain.Exceptions.Implementation;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Action = Domain.Models.Action;

namespace Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly DataBaseContext _dbContext;

    public RecipeRepository( DataBaseContext dbContext )
    {
        _dbContext = dbContext;
    }

    public Task<List<RecipeEntity>> GetAll( int start, int end )
    {
        return _dbContext.Recipes
            .OrderByDescending( x => x.RecipeId )
            .Skip( start - 1 )
            .Take( end - start + 1 )
            .ToListAsync();
    }

    public Task<RecipeEntity?> GetById( int id )
    {
        return _dbContext.Recipes.SingleOrDefaultAsync( recipe => id.Equals( recipe.RecipeId ) );
    }

    public async void Create( RecipeEntity entity )
    {
        await _dbContext.Recipes.AddAsync( entity );
    }

    public void Update( RecipeEntity entity )
    {
        _dbContext.Recipes.Update( entity );
    }

    public void Delete( RecipeEntity entity )
    {
        _dbContext.Recipes.Remove( entity );
    }

    public async Task<RecipeEntity?> GetBestRecipe( Action action )
    {
        int currentDay = DateTimeOffset.Now.DayOfYear;
        IQueryable<int> actions = _dbContext.RecipeActions
            .Where( entity => entity.ActionDay == currentDay )
            .Where( entity => entity.Action == action )
            .GroupBy( x => x.RecipeId )
            .OrderByDescending( x => x.Count() )
            .Select( x => x.Key );

        int recipeId = await actions.FirstOrDefaultAsync();
        if ( recipeId == 0 )
        {
            throw new NoBestRecipeException();
        }

        return await GetById( recipeId );
    }

    public async Task<List<RecipeEntity>> GetRecipesBySearchQuery( string searchQuery, int start, int end )
    {
        string lowerSearchQuery = searchQuery.ToLower();
        
        IQueryable<int> recipesByName = _dbContext.Recipes
            .Where( x => x.RecipeName.ToLower().Contains( lowerSearchQuery ) )
            .Select( x => x.RecipeId );

        IQueryable<int> recipesByTag = _dbContext.Tags
            .Where( x => x.Name.ToLower().Contains( lowerSearchQuery ) )
            .Include( x => x.Recipes )
            .SelectMany( x => x.Recipes, ( entity, recipeEntity ) => recipeEntity.RecipeId )
            .Distinct();

        List<int> totalRecipeIds = await recipesByName
            .Union( recipesByTag )
            .OrderByDescending( id => id )
            .Skip( start - 1 )
            .Take( end - start + 1 )
            .ToListAsync();

        List<RecipeEntity> totalRecipes = await _dbContext.Recipes
            .Where( x => totalRecipeIds.Contains( x.RecipeId ) )
            .ToListAsync();

        return totalRecipes;
    }
}
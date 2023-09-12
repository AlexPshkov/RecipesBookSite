using Domain.Exceptions.Implementation;
using Domain.Models;
using Domain.Models.secondary;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _dbContext;

    public UserRepository( DataBaseContext dbContext )
    {
        _dbContext = dbContext;
    }

    public Task<UserEntity?> GetById( Guid id )
    {
        return _dbContext.UserAccounts.SingleOrDefaultAsync( user => id.Equals( user.UserId ) );
    }

    public Task<UserEntity?> GetByLogin( string login )
    {
        return _dbContext.UserAccounts.SingleOrDefaultAsync( user => login == user.Login );
    }

    public async Task<List<RecipeEntity>> GetCreatedRecipes( Guid userId, int start, int end )
    {
        UserEntity? user = await _dbContext.UserAccounts
            .Include( x => x.CreatedRecipes )
            .SingleOrDefaultAsync( user => userId.Equals( user.UserId ) );
        if ( user == null )
        {
            throw new NoSuchUserException();
        }

        List<RecipeEntity> recipes = user.CreatedRecipes
            .OrderByDescending( x => x.RecipeId )
            .Skip( start - 1 )
            .Take( end - start + 1 )
            .ToList();

        return recipes;
    }

    public async Task<List<RecipeEntity>> GetFavorites( Guid userId, int start, int end )
    {
        UserEntity? user = await _dbContext.UserAccounts
            .Include( x => x.Favorites )
            .SingleOrDefaultAsync( user => userId.Equals( user.UserId ) );
        if ( user == null )
        {
            throw new NoSuchUserException();
        }

        List<FavoriteEntity> favorites = user.Favorites
            .OrderByDescending( x => x.RecipeId )
            .Skip( start - 1 )
            .Take( end - start + 1 )
            .ToList();

        return favorites.ConvertAll( input => input.Recipe );
    }

    public async Task<UserStatisticEntity> GetUserStatistic( Guid userId )
    {
        UserEntity? user = await _dbContext.UserAccounts
            .Include( x => x.CreatedRecipes )
            .SingleOrDefaultAsync( user => userId.Equals( user.UserId ) );
        if ( user == null )
        {
            throw new NoSuchUserException();
        }

        List<RecipeEntity> recipes = user.CreatedRecipes
            .OrderByDescending( x => x.RecipeId )
            .ToList();

        int totalLikes = 0;
        int totalFavorites = 0;
        recipes.ForEach( x => totalLikes += x.Likes.Count );
        recipes.ForEach( x => totalFavorites += x.Favorites.Count );

        return new UserStatisticEntity
        {
            CreatedRecipesAmount = recipes.Count,
            CreatedRecipesLikesAmount = totalLikes,
            CreatedRecipesFavoritesAmount = totalFavorites
        };
    }

    public async void Create( UserEntity entity )
    {
        await _dbContext.UserAccounts.AddAsync( entity );
    }

    public void Update( UserEntity entity )
    {
        _dbContext.UserAccounts.Update( entity );
    }

    public void Delete( UserEntity entity )
    {
        _dbContext.UserAccounts.Remove( entity );
    }
}
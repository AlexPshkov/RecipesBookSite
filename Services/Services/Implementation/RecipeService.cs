using Domain;
using Domain.Exceptions.Implementation;
using Domain.Models;
using Domain.Models.secondary;
using Domain.Repositories;
using Services.Extensions.Entity;
using Action = Domain.Models.Action;

namespace Services.Services.Implementation;

public class RecipeService : IRecipeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRecipeRepository _repository;
    private readonly ITagRepository _tagRepository;

    public RecipeService( IUnitOfWork unitOfWork, IRecipeRepository repository, ITagRepository tagRepository )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _tagRepository = tagRepository;
    }

    public Task<List<RecipeEntity>> GetAllRecipes( int start, int end )
    {
        return _repository.GetAll( start, end );
    }

    public async Task<RecipeEntity> GetRecipe( int recipeId )
    {
        RecipeEntity? recipe = await _repository.GetById( recipeId );
        if ( recipe == null )
        {
            throw new NoSuchRecipeException( recipeId );
        }

        recipe.Actions.Add( recipe.ConvertToRecipeActionEntity( Action.View ) );
        await _unitOfWork.SaveChanges();

        return recipe;
    }

    private async Task<List<TagEntity>> GetDomainTags( List<TagEntity> tags )
    {
        List<TagEntity> list = new List<TagEntity>();
        foreach ( TagEntity tagEntity in tags )
        {
            TagEntity? domainTag = await _tagRepository.GetByName( tagEntity.Name );
            list.Add( domainTag ?? tagEntity );
        }

        return list;
    }

    public async Task<int> SaveRecipe( RecipeEntity newRecipeEntity )
    {
        newRecipeEntity.Tags = await GetDomainTags( newRecipeEntity.Tags );

        if ( newRecipeEntity.RecipeId == 0 )
        {
            _repository.Create( newRecipeEntity );
            await _unitOfWork.SaveChanges();

            return newRecipeEntity.RecipeId;
        }

        RecipeEntity? domainRecipe = await _repository.GetById( newRecipeEntity.RecipeId );

        if ( domainRecipe == null )
        {
            throw new NoSuchRecipeException( newRecipeEntity.RecipeId );
        }

        if ( newRecipeEntity.RecipeId != domainRecipe.RecipeId )
        {
            throw new NoPermException( "Another RecipeId" );
        }

        if ( newRecipeEntity.UserId != domainRecipe.UserId )
        {
            throw new NoPermException( "Another UserId" );
        }

        _repository.Update( domainRecipe.Combine( newRecipeEntity ) );
        await _unitOfWork.SaveChanges();

        return newRecipeEntity.RecipeId;
    }

    public async Task<bool> RemoveRecipe( int recipeId, Guid userId )
    {
        RecipeEntity? recipeEntity = await _repository.GetById( recipeId );

        if ( recipeEntity == null )
        {
            throw new NoSuchRecipeException( recipeId );
        }

        if ( recipeEntity.UserId != userId )
        {
            throw new NoPermException( "Another userId" );
        }

        _repository.Delete( recipeEntity );

        return await _unitOfWork.SaveChanges();
    }

    public async Task<RecipeEntity> HandleLike( int recipeId, Guid userId )
    {
        RecipeEntity? recipe = await _repository.GetById( recipeId );
        if ( recipe == null )
        {
            throw new NoSuchRecipeException();
        }

        LikeEntity? domainLike = recipe.Likes.Find( input => input.UserId.Equals( userId ) );

        LikeEntity likeEntity = new LikeEntity
        {
            LikeId = domainLike?.LikeId ?? 0,
            Recipe = recipe,
            UserId = userId
        };

        if ( domainLike != null )
        {
            recipe.Likes.Remove( domainLike );
        }
        else
        {
            recipe.Likes.Add( likeEntity );
        }

        recipe.Actions.Add( recipe.ConvertToRecipeActionEntity( Action.Like ) );

        await _unitOfWork.SaveChanges();

        return recipe;
    }

    public async Task<RecipeEntity> HandleFavorite( int recipeId, Guid userId )
    {
        RecipeEntity? recipe = await _repository.GetById( recipeId );
        if ( recipe == null )
        {
            throw new NoSuchRecipeException();
        }

        FavoriteEntity? domainFavorite = recipe.Favorites.Find( input => input.UserId.Equals( userId ) );

        FavoriteEntity favoriteEntity = new FavoriteEntity
        {
            FavoriteId = domainFavorite?.FavoriteId ?? 0,
            Recipe = recipe,
            UserId = userId
        };

        if ( domainFavorite != null )
        {
            recipe.Favorites.Remove( domainFavorite );
        }
        else
        {
            recipe.Favorites.Add( favoriteEntity );
        }

        recipe.Actions.Add( recipe.ConvertToRecipeActionEntity( Action.Favorite ) );
        await _unitOfWork.SaveChanges();

        return recipe;
    }

    public async Task<RecipeEntity> GetBestRecipe( Action action )
    {
        RecipeEntity? recipe = await _repository.GetBestRecipe( action );
        if ( recipe == null )
        {
            throw new NoSuchRecipeException();
        }

        return recipe;
    }

    public Task<List<RecipeEntity>> GetRecipesBySearchQuery( string searchQuery, int start, int end )
    {
        return _repository.GetRecipesBySearchQuery( searchQuery, start, end );
    }

    public async Task<List<TagEntity>> GetBestTags( int amount )
    {
        List<TagEntity> tags = await _tagRepository.GetBestTags( amount );

        return tags;
    }
}
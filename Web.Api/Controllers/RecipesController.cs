using System.Security.Authentication;
using System.Security.Claims;
using Domain.Models;
using Domain.Models.secondary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dto.Recipe;
using Services.Dto.Responses;
using Services.Extensions.Dto;
using Services.Extensions.Entity;
using Services.Services;
using Web.Api.Filters;
using Action = Domain.Models.Action;

namespace Web.Api.Controllers;

[ApiController]
[Route( "api/[controller]" )]
[TypeFilter( typeof( ExceptionsFilter ) )]
public class RecipesController : Controller
{
    private Guid? UserId
    {
        get
        {
            bool isAuthed = User.Claims.Any( x => x.Type == ClaimTypes.NameIdentifier );
            if ( isAuthed )
            {
                return Guid.Parse( User.Claims.Single( c => c.Type == ClaimTypes.NameIdentifier ).Value );
            }
            return null;
        }
    }

    private readonly ILogger<RecipesController> _logger;
    private readonly IRecipeService _recipeService;

    public RecipesController( IRecipeService recipeService, ILogger<RecipesController> logger )
    {
        _logger = logger;
        _recipeService = recipeService;
    }

    [HttpGet]
    [Route( "{recipeId:int}" )]
    public async Task<IActionResult> Get( int recipeId )
    {
        _logger.LogInformation( "Trying to get recipe with ID: {RecipeId}", recipeId );
        
        RecipeEntity recipe = await _recipeService.GetRecipe( recipeId );
        _logger.LogInformation( "Success! Recipe with ID: {RecipeId} successfully got", recipeId );
        
        return Ok( recipe.ConvertToRecipeDto( UserId ) );
    }
    
    [Authorize]
    [HttpDelete]
    [Route( "{recipeId:int}" )]
    public async Task<IActionResult> Delete( int recipeId )
    {
        _logger.LogInformation( "Trying to remove recipe with ID: {RecipeId}", recipeId );
        await _recipeService.RemoveRecipe( recipeId, UserId.GetValueOrDefault( Guid.Empty ) );
        _logger.LogInformation( "Success! Recipe with ID: {RecipeId} successfully removed", recipeId );
        
        return Ok();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Save( RecipeDto recipeDto )
    {
        Guid userId = UserId ?? throw new AuthenticationException();
        _logger.LogInformation( "Trying to save recipe with ID: {RecipeId}. Recipe author ID: {UserId}", recipeDto.Id, userId );
        
        RecipeEntity recipeEntity = recipeDto.ConvertToRecipeEntity( userId );
        int createdId = await _recipeService.SaveRecipe( recipeEntity );
        
        _logger.LogInformation( "Success! Recipe with ID: {RecipeId} successfully saved. Recipe author ID: {UserId}", recipeDto.Id, userId );
        return Ok( new RecipeCreated
        {
            RecipeId = createdId
        } );
    }

    [HttpPut]
    [Authorize]
    [Route( "like/{recipeId:int}" )]
    public async Task<IActionResult> Like( int recipeId )
    {
        Guid userId = UserId ?? throw new AuthenticationException();
        _logger.LogInformation( "Trying to put like on recipe with ID: {RecipeId}. Customer ID: {UserId}", recipeId, userId );
        
        RecipeEntity recipe = await _recipeService.HandleLike( recipeId, userId );
        
        _logger.LogInformation( "Success! Like put on recipe with ID: {RecipeId}. Customer ID: {UserId}", recipeId, userId );
        return Ok( recipe.ConvertToRecipeDto( userId ) );
    }

    [HttpPut]
    [Authorize]
    [Route( "favorite/{recipeId:int}" )]
    public async Task<IActionResult> Favorite( int recipeId )
    {
        Guid userId = UserId ?? throw new AuthenticationException();
        _logger.LogInformation( "Trying to make recipe with ID: {RecipeId} favorite. Customer ID: {UserId}", recipeId, userId );
        
        RecipeEntity recipe = await _recipeService.HandleFavorite( recipeId, userId );
        
        _logger.LogInformation( "Success! Recipe with ID: {RecipeId} favorite success. Customer ID: {UserId}", recipeId, userId );
        return Ok( recipe.ConvertToRecipeDto( UserId ) );
    }
    
    [HttpGet]
    [Route( "best-recipe" )]
    public async Task<IActionResult> BestRecipe( int recipeId )
    {
        _logger.LogInformation( "Trying to get best recipe of the day" );
        RecipeEntity bestRecipe = await _recipeService.GetBestRecipe( Action.View );
        
        _logger.LogInformation( "Success! Recipe of the day got. Recipe ID: {RecipeId}", bestRecipe.RecipeId );
        return Ok( bestRecipe.ConvertToRecipeDto( UserId ) );
    }
    
    [HttpGet]
    [Route( "search" )]
    public async Task<IActionResult> GetFromAll( int start = 1, int end = 4 )
    {
        _logger.LogInformation( "Trying to get recipes from {Start} to {End}. No specific query", start, end );
        
        List<RecipeEntity> recipes = await _recipeService.GetAllRecipes( start, end );
        _logger.LogInformation( "Success! Recipes from {Start} to {End} successfully got. No specific query", start, end );
        
        return Ok( recipes.ConvertAll( input => input.ConvertToRecipeDto( UserId ) ) );
    }
    
    [HttpGet]
    [Route( "search/{searchQuery}" )]
    public async Task<IActionResult> GetByQuery( string searchQuery, int start = 1, int end = 4 )
    {
        _logger.LogInformation( "Trying to get recipes from {Start} to {End}. Query: {SearchQuery}", start, end, searchQuery );
        
        List<RecipeEntity> recipes = await _recipeService.GetRecipesBySearchQuery( searchQuery, start, end );
        _logger.LogInformation( "Success! Recipes from {Start} to {End} successfully got. Query: {SearchQuery}", start, end, searchQuery );
        
        
        return Ok( recipes.ConvertAll( x => x.ConvertToRecipeDto( UserId ) ) );
    }
    
    [HttpGet]
    [Route( "best-tags" )]
    public async Task<IActionResult> BestTags( int amount = 5 )
    {
        _logger.LogInformation( "Trying to get best tags. Amount {Amount}", amount );
        List<TagEntity> bestTags = await _recipeService.GetBestTags( amount );
        
        _logger.LogInformation( "Success! Best tags got. Amount: {Amount}", amount );
        return Ok( bestTags.ConvertAll( input => input.ConvertToTagDto() ) );
    }
}
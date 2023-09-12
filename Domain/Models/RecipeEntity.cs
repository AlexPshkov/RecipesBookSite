using Domain.Models.secondary;
using Domain.Validators;

namespace Domain.Models;

public class RecipeEntity : AbstractEntity
{
    public int RecipeId { get; set; }

    private string _recipeName = "";
    public string RecipeName
    {
        get => _recipeName;
        set => _recipeName = RecipesValidators.ValidateName( value );
    }
    
    private string _recipeDescription = "";
    public string RecipeDescription
    {
        get => _recipeDescription;
        set => _recipeDescription = RecipesValidators.ValidateDescription( value );
    }
    
    private string _imagePath = "";
    public string ImagePath
    {
        get => _imagePath;
        set => _imagePath = RecipesValidators.ValidateImagePath( value );
    }
    
    private string _requiredTime = "";
    public string RequiredTime
    {
        get => _requiredTime;
        set => _requiredTime = RecipesValidators.ValidateRequiredTime( value );
    }
    
    private string _servingsAmount = "";
    public string ServingsAmount
    {
        get => _servingsAmount;
        set => _servingsAmount = RecipesValidators.ValidateServingsAmount( value );
    }
    
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    
    public List<FavoriteEntity> Favorites { get; set; } = new List<FavoriteEntity>();
    public List<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
    
    public List<IngredientEntity> Ingredients { get; set; } = new List<IngredientEntity>();
    public List<StepEntity> Steps { get; set; } = new List<StepEntity>();
    public List<TagEntity> Tags { get; set; } = new List<TagEntity>();
    public List<RecipeActionEntity> Actions { get; } = new List<RecipeActionEntity>();
}


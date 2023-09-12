namespace Services.Dto.Recipe;

public class RecipeDto
{
    public int Id { get; init; }
    public string RecipeName { get; init; }
    public string RecipeDescription { get; init; }

    public string ImagePath { get; init; }
    public string RequiredTime { get; init; }
    public string ServingsAmount { get; init; }
    
    public string UserLogin { get; init; } = "";
    public int FavoritesAmount { get; init; }
    public int LikesAmount { get; init; }
    public bool IsCreator { get; init; }
    public bool IsLiked { get; init; }
    public bool IsFavorite { get; init; }
    
    public List<TagDto> Tags { get; init; }
    public List<IngredientDto> Ingredients { get; init; }
    public List<StepDto> Steps { get; init; }
}
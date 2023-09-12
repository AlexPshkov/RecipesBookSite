namespace Services.Dto.Recipe;

public class FavoriteDto
{
    public int Id { get; init; }
    public int RecipeId { get; init; }
    public string UserId { get; init; }
}
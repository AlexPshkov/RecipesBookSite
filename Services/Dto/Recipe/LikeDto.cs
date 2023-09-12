namespace Services.Dto.Recipe;

public class LikeDto
{
    public int Id { get; init; }
    public int RecipeId { get; init; }
    public string UserId { get; init; }
}
namespace Domain.Models.secondary;

public class IngredientEntity : AbstractEntity
{
    public int IngredientId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int RecipeId { get; set; }
    
    public RecipeEntity? Recipe { get; set; }
}

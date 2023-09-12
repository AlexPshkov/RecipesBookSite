namespace Domain.Models.secondary;

public class TagEntity : AbstractEntity
{
    public int TagId { get; set; }
    public string Name { get; set; }

    public List<RecipeEntity> Recipes { get; } = new List<RecipeEntity>();
}

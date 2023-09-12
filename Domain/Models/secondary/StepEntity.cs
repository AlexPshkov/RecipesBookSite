namespace Domain.Models.secondary;

public class StepEntity : AbstractEntity
{
    public int StepId { get; set; }
    public string Description { get; set; }
    public int RecipeId { get; set; }
    
    public RecipeEntity? Recipe { get; set; }
}

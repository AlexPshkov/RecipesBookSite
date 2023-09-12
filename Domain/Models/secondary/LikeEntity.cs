namespace Domain.Models.secondary;

public class LikeEntity : AbstractEntity
{
    public int LikeId { get; set; }
    public Guid UserId { get; set; }
    public int RecipeId { get; set; }
    
    public UserEntity? User { get; set; }
    public RecipeEntity? Recipe { get; set; }
}

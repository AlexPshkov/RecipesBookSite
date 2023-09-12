namespace Domain.Models;

public class UserStatisticEntity : AbstractEntity
{
    public int CreatedRecipesAmount { get; set; }
    public int CreatedRecipesLikesAmount { get; set; }
    public int CreatedRecipesFavoritesAmount { get; set; }
}
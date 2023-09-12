using Domain.Models.secondary;
using Domain.Validators;

namespace Domain.Models;

public class UserEntity : AbstractEntity
{
    public Guid UserId { get; set; }

    private string _userName = "";
    public string UserName
    {
        get => _userName;
        set => _userName = UserValidators.ValidateName( value );
    }
    
    private string _login = "";
    public string Login
    {
        get => _login;
        set => _login = UserValidators.ValidateLogin( value );
    }

    private string _password = "";
    public string Password
    {
        get => _password;
        set => _password = UserValidators.ValidatePassword( value );
    }
    
    private string _description = "";
    public string Description
    {
        get => _description;
        set => _description = UserValidators.ValidateDescription( value );
    }
    
    public Role Role { get; set; }
    
    public List<RecipeEntity> CreatedRecipes { get; } = new List<RecipeEntity>();
    public List<LikeEntity> Likes { get; } = new List<LikeEntity>();
    public List<FavoriteEntity> Favorites { get; } = new List<FavoriteEntity>();
}

public enum Role
{
    User,
    Admin
}
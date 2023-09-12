using Domain.Models;
using Domain.Models.secondary;
using Infrastructure.EntitiesConfigurations;
using Infrastructure.EntitiesConfigurations.secondary;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataBaseContext : DbContext
{
    public DbSet<UserEntity> UserAccounts { get; set; }
    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<RecipeActionEntity> RecipeActions { get; set; }

    public DbSet<FavoriteEntity> Favorites { get; set; }
    public DbSet<LikeEntity> Likes { get; set; }
    public DbSet<StepEntity> Steps { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<IngredientEntity> Ingredients { get; set; }


    public DataBaseContext( DbContextOptions<DataBaseContext> options ) : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        builder.ApplyConfiguration( new UserEntityMap() );
        builder.ApplyConfiguration( new RecipeEntityMap() );

        builder.ApplyConfiguration( new FavoriteEntityMap() );
        builder.ApplyConfiguration( new LikeEntityMap() );
        builder.ApplyConfiguration( new StepEntityMap() );
        builder.ApplyConfiguration( new IngredientEntityMap() );
        builder.ApplyConfiguration( new TagEntityMap() );
        builder.ApplyConfiguration( new RecipeActionEntityMap() );
    }
}
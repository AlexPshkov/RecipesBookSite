using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class RecipeEntityMap : IEntityTypeConfiguration<RecipeEntity>
{
    public void Configure( EntityTypeBuilder<RecipeEntity> builder )
    {
        builder.HasKey( x => x.RecipeId );
        builder.Property( x => x.RecipeId ).ValueGeneratedOnAdd();

        builder.Property( x => x.RecipeDescription ).HasMaxLength( 150 );
        builder.Property( x => x.RecipeName ).HasMaxLength( 150 );
        builder.Property( x => x.ImagePath ).HasMaxLength( 550 );
        builder.Property( x => x.RequiredTime ).HasMaxLength( 50 );
        builder.Property( x => x.ServingsAmount ).HasMaxLength( 50 );
        builder.Property( x => x.UserId );

        builder.HasOne( x => x.User )
            .WithMany( p => p.CreatedRecipes );

        builder.Navigation( p => p.User ).AutoInclude();
        builder.Navigation( p => p.Likes ).AutoInclude();
        builder.Navigation( p => p.Favorites ).AutoInclude();
        builder.Navigation( p => p.Steps ).AutoInclude();
        builder.Navigation( p => p.Tags ).AutoInclude();
        builder.Navigation( p => p.Ingredients ).AutoInclude();
    }
} 
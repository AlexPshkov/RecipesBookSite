using Domain.Models.secondary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations.secondary;

public class IngredientEntityMap : IEntityTypeConfiguration<IngredientEntity>
{
    public void Configure( EntityTypeBuilder<IngredientEntity> builder )
    {
        builder.HasKey( x => x.IngredientId );
        builder.Property( x => x.IngredientId ).ValueGeneratedOnAdd();
        
        builder.Property( x => x.Title ).HasMaxLength( 500 );
        builder.Property( x => x.Description ).HasMaxLength( 1000 );
        builder.Property( x => x.RecipeId );
        
        builder.HasOne( x => x.Recipe )
            .WithMany( p => p.Ingredients );
        
        builder.Navigation( p => p.Recipe ).AutoInclude();
    }
}

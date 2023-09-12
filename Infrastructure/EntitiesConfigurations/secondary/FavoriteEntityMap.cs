using Domain.Models.secondary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations.secondary;

public class FavoriteEntityMap : IEntityTypeConfiguration<FavoriteEntity>
{
    public void Configure( EntityTypeBuilder<FavoriteEntity> builder )
    {
        builder.HasKey( x => x.FavoriteId );
        builder.Property( x => x.FavoriteId ).ValueGeneratedOnAdd();

        builder.Property( x => x.UserId );
        builder.Property( x => x.RecipeId );
        
        builder.HasOne( x => x.User )
            .WithMany( p => p.Favorites );
        builder.HasOne( x => x.Recipe )
            .WithMany( p => p.Favorites );

        builder.Navigation( p => p.Recipe ).AutoInclude();
        builder.Navigation( p => p.User ).AutoInclude();
    }
}

using Domain.Models.secondary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations.secondary;

public class LikeEntityMap : IEntityTypeConfiguration<LikeEntity>
{
    public void Configure( EntityTypeBuilder<LikeEntity> builder )
    {
        builder.HasKey( x => x.LikeId );
        builder.Property( x => x.LikeId ).ValueGeneratedOnAdd();

        builder.Property( x => x.UserId );
        builder.Property( x => x.RecipeId );
        
        builder.HasOne( x => x.User )
            .WithMany( p => p.Likes );
        builder.HasOne( x => x.Recipe )
            .WithMany( p => p.Likes );
        
        builder.Navigation( p => p.Recipe ).AutoInclude();
        builder.Navigation( p => p.User ).AutoInclude();
    }
}

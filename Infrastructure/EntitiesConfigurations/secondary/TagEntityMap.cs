using Domain.Models.secondary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations.secondary;

public class TagEntityMap : IEntityTypeConfiguration<TagEntity>
{
    public void Configure( EntityTypeBuilder<TagEntity> builder )
    {
        builder.HasKey( x => x.TagId );
        builder.Property( x => x.TagId ).ValueGeneratedOnAdd();

        builder.Property( x => x.Name ).HasMaxLength( 500 );

        builder.HasIndex( x => x.Name );
        
        builder.HasMany( x => x.Recipes )
            .WithMany( p => p.Tags );
    }
}
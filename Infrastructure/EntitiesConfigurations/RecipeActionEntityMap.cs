using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class RecipeActionEntityMap : IEntityTypeConfiguration<RecipeActionEntity>
{
    public void Configure( EntityTypeBuilder<RecipeActionEntity> builder )
    {
        builder.HasKey( x => x.ActionId );
        builder.Property( x => x.ActionId ).ValueGeneratedOnAdd();

        builder.Property( x => x.Action );
        builder.Property( x => x.ActionDay );
        builder.Property( x => x.RecipeId );
        builder.Property( x => x.UserId );
    }
} 
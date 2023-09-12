using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class UserEntityMap : IEntityTypeConfiguration<UserEntity>
{
    public void Configure( EntityTypeBuilder<UserEntity> builder )
    {
        builder.HasKey( x => x.UserId );
        
        builder.Property( x => x.Login ).HasMaxLength( 100 );
        builder.Property( x => x.Password ).HasMaxLength( 500 );
        builder.Property( x => x.UserName ).HasMaxLength( 250 );
        builder.Property( x => x.Description ).HasMaxLength( 500 );
        builder.Property( x => x.Role );

        builder.HasIndex( x => x.Login );
    }
}
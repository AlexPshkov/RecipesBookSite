using Domain;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class AddInfrastructureExtensions
{
    public static void AddInfrastructure( this IServiceCollection services, string connectionString )
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        
        AddCustomDbContext( services, connectionString );
    }
    
    private static void AddCustomDbContext( IServiceCollection services, string connectionString )
    {
        services.AddDbContext<DataBaseContext>( c =>
        {
            c.UseNpgsql( connectionString, o => o.UseQuerySplittingBehavior( QuerySplittingBehavior.SplitQuery ) );
        } );
    }
}
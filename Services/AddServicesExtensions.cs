using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using Services.Services.Implementation;

namespace Services;

public static class AddServicesExtensions
{

    public static void AddServices( this IServiceCollection services )
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IImageService, ImageService>();
    }
}
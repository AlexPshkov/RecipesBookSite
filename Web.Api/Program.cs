using Domain.Models.Auth;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Extensions.Entity;

namespace Web.Api;

public static class Program
{
    public static void Main( string[] args )
    {
        WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder( args );

        applicationBuilder.Services.AddServices();
        applicationBuilder.Services.AddInfrastructure( applicationBuilder.Configuration.GetValue<string>( "SQLConnection" )! );
        applicationBuilder.AddCustomAuth();
        
        applicationBuilder.AddCustomCorsPolicy( "AllowAll" );
        
        applicationBuilder.Services.AddControllers();
        applicationBuilder.Services.AddEndpointsApiExplorer();

        BuildAndRun( applicationBuilder );
    }

    private static void BuildAndRun( WebApplicationBuilder applicationBuilder )
    {
        WebApplication app = applicationBuilder.Build();

        app.UseCors( "AllowAll" );
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();

        app.MapControllers();

        app.Run();
    }
    
    private static void AddCustomCorsPolicy( this WebApplicationBuilder builder, string policyName )
    {
        builder.Services.AddCors( options =>
        {
            options.AddPolicy( policyName,
                policyBuilder => policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
        } );
    }
    
    private static void AddCustomAuth( this WebApplicationBuilder builder )
    {
        AuthOptions? authOptions = builder.Configuration.GetSection( "Auth" ).Get<AuthOptions>();
        builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
        
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authOptions.Issuer,
                ValidAudience = authOptions.Audience,
                IssuerSigningKey = authOptions.GetSymmetricSecurityKey()
            };
        });
    } 
}
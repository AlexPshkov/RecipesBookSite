using Domain.Models.Auth;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

        string dbConnectionRawUrl = applicationBuilder.Configuration.GetValue<string>( "SQLConnection" )!;
        string dbConnectionUrl = string.Format( dbConnectionRawUrl,
            Environment.GetEnvironmentVariable( "DB_SERVER" ),
            Environment.GetEnvironmentVariable( "DB_PORT" ),
            Environment.GetEnvironmentVariable( "DB_USER" ),
            Environment.GetEnvironmentVariable( "DB_PASSWORD" ) );
        applicationBuilder.Services.AddInfrastructure( dbConnectionUrl );

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
        
        app.MakeMigrations();

        app.Run();
    }

    private static void MakeMigrations( this IHost webApplication )
    {
        using IServiceScope scope = webApplication.Services.CreateScope();
        using DataBaseContext context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

        context.Database.Migrate();

        context.SaveChanges();
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
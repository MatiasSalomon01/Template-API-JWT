using Core;
using Core.Models.ModelOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration config)
    {

        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerCustomGen();

        services.AddOptions(config);
        services.AddJwtAuth();

        return services;
    }

    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config.GetSection(Constants.JwtOptions));

        return services;
    }

    public static IServiceCollection AddJwtAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;
                var keyInBytes = Encoding.UTF8.GetBytes(jwtOptions.Key);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(keyInBytes),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();
        
        return services;
    }
    
    public static IServiceCollection AddSwaggerCustomGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
         {
             var jwtSecurityScheme = new OpenApiSecurityScheme
             {
                 BearerFormat = "JWT",
                 Name = JwtBearerDefaults.AuthenticationScheme,
                 In = ParameterLocation.Header, 
                 Type = SecuritySchemeType.Http,
                 Scheme = JwtBearerDefaults.AuthenticationScheme,
                 Description = "Ingrese el token generado en auth/login",
                 Reference = new OpenApiReference
                 {
                     Id = JwtBearerDefaults.AuthenticationScheme,
                     Type = ReferenceType.SecurityScheme
                 }
             };

             setup.AddSecurityDefinition(jwtSecurityScheme.Name, jwtSecurityScheme);

             setup.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 { jwtSecurityScheme, Array.Empty<string>() }
             });
         });

        return services;
    }
}

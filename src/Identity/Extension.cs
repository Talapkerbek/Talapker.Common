using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Talapker.Common.Identity;

public static class Extension
{
    public static AuthenticationBuilder AddJwtBearerAuthentication(
        this IServiceCollection services,
        Action<JwtBearerOptions>? configureOptions = null    
    )
    {
        var builder = services.ConfigureOptions<ConfigureJwtBearerOptions>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        if (configureOptions != null)
        {
            builder.Services.PostConfigure(configureOptions);
        }
        
        return builder;
    }
}
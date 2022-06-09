using forgedinthelore_net.Data;
using forgedinthelore_net.Entities;
using forgedinthelore_net.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace forgedinthelore_net.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(opt =>
            {
                //This is default but as an example of how to configure
                opt.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<DataContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //For normal validation
                options.TokenValidationParameters = TokenValidationParameterOptions.GetParameters(config);
        
                //For SignalR validation
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
        
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
        
                        return Task.CompletedTask;
                    }
                };
            });

        //Use this to configure policies that can be used in API endpoints
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("IsAdmin", policy => policy.RequireRole("Admin"));
            opt.AddPolicy("IsUser", policy => policy.RequireRole("User","Admin"));
        });

        return services;
    }

    
}
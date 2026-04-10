using Application.Abstractions.Identity;
using GymPortal.Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Identity.Extensions;

public static class IdentityServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(x =>
        {
            x.SignIn.RequireConfirmedAccount = false;
            x.User.RequireUniqueEmail = true;
            x.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<PersistenceContext>()
        .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(x =>
        {
            x.LoginPath = "/sign-in";
            x.LogoutPath = "/";
            x.AccessDeniedPath = "/denied";

            x.Cookie.IsEssential = true;
            x.Cookie.Name = "corefitness_auth";
            x.ExpireTimeSpan = TimeSpan.FromDays(30);
            x.SlidingExpiration = true;
        });

        services.AddScoped<IAuthService, IdentityAuthService>();
        services.AddScoped<IAccountService, IdentityAccountService>();

        return services;
    }
}

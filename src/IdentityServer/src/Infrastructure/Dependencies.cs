using DuongTruong.IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DuongTruong.IdentityServer.Infrastructure
{
    public static class Dependencies
    {
        public static IdentityBuilder AddCustomIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction)
        {
            return services.AddIdentity<ApplicationUser, ApplicationRole>(setupAction)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
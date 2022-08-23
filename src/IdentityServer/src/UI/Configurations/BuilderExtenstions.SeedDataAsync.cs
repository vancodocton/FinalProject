using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.Infrastructure.IdentityServer;
using System.Diagnostics;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static partial class BuilderExtenstions
    {
        public static async Task<IServiceProvider> SeedDataAsync(this IServiceProvider services)
        {
            var watch = services.GetRequiredService<Stopwatch>();
            var logger = services.GetRequiredService<ILogger<Stopwatch>>();

            await services.InitialConfigurationDataAsync<ConfigurationDbContext>(
                IdentityServerConfigurations.IdentityResources,
                IdentityServerConfigurations.ApiScopes,
                IdentityServerConfigurations.Clients);
            logger.LogInformation("InitialConfigurationDataAsync elapsed {time} s.", watch.ElapsedMilliseconds / 1000.0);

            await services.InitialIdentityRolesAsync<ApplicationRole>(Utils.Role.ToArray());
            logger.LogInformation("InitialIdentityRolesAsync elapsed {time} s.", watch.ElapsedMilliseconds / 1000.0);

            return services;
        }
    }
}

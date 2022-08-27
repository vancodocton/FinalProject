using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.Infrastructure.IdentityServer;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static partial class BuilderExtenstions
    {
        public static async Task<IServiceProvider> SeedDataAsync(this IServiceProvider services)
        {
            var watch = services.GetRequiredService<Stopwatch>();

            using (var dbContext = services.GetRequiredService<ConfigurationDbContext>())
            {
                var logger = services.GetRequiredService<ILogger<ConfigurationDbContext>>();

                var rows = await dbContext.InitialConfigurationDataAsync(
                    IdentityServerConfigurations.IdentityResources,
                    IdentityServerConfigurations.ApiScopes,
                    IdentityServerConfigurations.Clients);

                logger.LogInformation("Added {rows} row(s) to configuration database.", rows);
                logger.LogInformation("InitialConfigurationDataAsync elapsed {time} s.", watch.ElapsedMilliseconds / 1000.0);
            }

            using (var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>())
            {
                await roleManager.InitialRolesAsync(Utils.Role.ToArray());
                roleManager.Logger.LogInformation("InitialIdentityRolesAsync elapsed {time} s.", watch.ElapsedMilliseconds / 1000.0);
            }

            return services;
        }
    }
}

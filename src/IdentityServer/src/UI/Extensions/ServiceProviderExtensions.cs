using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using DuongTruong.IdentityServer.UI;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static async Task<IServiceProvider> InitializeDataAsync(this IServiceProvider serviceProvider)
        {
            await IdentityServer.InitialConfigurationData(serviceProvider.GetRequiredService<ConfigurationDbContext>());

            return serviceProvider;
        }

        public static class IdentityServer
        {
            public static async Task InitialConfigurationData(ConfigurationDbContext dbContext)
            {

                dbContext.Database.Migrate();

                if (!await dbContext.Clients.AnyAsync())
                {
                    foreach (var client in Config.Clients)
                    {
                        dbContext.Clients.Add(client.ToEntity());
                    }
                    dbContext.SaveChanges();
                }


                if (!await dbContext.IdentityResources.AnyAsync())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        dbContext.IdentityResources.Add(resource.ToEntity());
                    }
                    await dbContext.SaveChangesAsync();
                }

                if (!await dbContext.ApiScopes.AnyAsync())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        dbContext.ApiScopes.Add(resource.ToEntity());
                    }
                    await dbContext.SaveChangesAsync();
                }

                return;
            }
        }
    }
}

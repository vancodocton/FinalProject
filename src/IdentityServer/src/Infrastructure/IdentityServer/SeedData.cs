using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DuongTruong.IdentityServer.Infrastructure.IdentityServer;

    public static class SeedData
    {
        public static async Task<IServiceProvider> InitialConfigurationDataAsync<T>(this IServiceProvider services,
            IEnumerable<Duende.IdentityServer.Models.IdentityResource> identityResources,
            IEnumerable<Duende.IdentityServer.Models.ApiScope> apiScopes,
            IEnumerable<Duende.IdentityServer.Models.Client> clients)
            where T : DbContext, IConfigurationDbContext
        {
            var dbContext = services.GetRequiredService<T>();
        var logger = services.GetRequiredService<ILogger<T>>();

        var rows = 0;
            foreach (var resource in identityResources)
            {
            var isExisted = await dbContext.IdentityResources.AnyAsync(x => x.Name == resource.Name);
            if (!isExisted)
                dbContext.IdentityResources.Add(resource.ToEntity());
            }
        rows += await dbContext.SaveChangesAsync();

            foreach (var apiScope in apiScopes)
            {
            var isExisted = await dbContext.ApiScopes.AnyAsync(x => x.Name == apiScope.Name);

            if (!isExisted)
                await dbContext.ApiScopes.AddAsync(apiScope.ToEntity());
            }
        rows += await dbContext.SaveChangesAsync();

            foreach (var client in clients)
            {
            var isExisted = await dbContext.Clients.AnyAsync(x => x.ClientId == client.ClientId);
            if (!isExisted)
                await dbContext.Clients.AddAsync(client.ToEntity());
            }
        rows += await dbContext.SaveChangesAsync();

        logger.LogInformation($"Added {rows} row(s) to configuration database.");

            return services;
        }
    }


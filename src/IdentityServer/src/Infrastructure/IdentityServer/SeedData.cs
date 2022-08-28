using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.Infrastructure.IdentityServer;

public static class SeedData
{
    public static async Task<int> InitialConfigurationDataAsync<T>(this T dbContext,
        IEnumerable<Duende.IdentityServer.Models.IdentityResource> identityResources,
        IEnumerable<Duende.IdentityServer.Models.ApiScope> apiScopes,
        IEnumerable<Duende.IdentityServer.Models.Client> clients)
        where T : DbContext, IConfigurationDbContext
    {
        var rows = 0;
        foreach (var resource in identityResources)
        {
            var isExisted = await dbContext.IdentityResources.AnyAsync(x => x.Name == resource.Name);
            if (!isExisted)
                dbContext.IdentityResources.Add(resource.ToEntity());
        }

        foreach (var apiScope in apiScopes)
        {
            var isExisted = await dbContext.ApiScopes.AnyAsync(x => x.Name == apiScope.Name);

            if (!isExisted)
                await dbContext.ApiScopes.AddAsync(apiScope.ToEntity());
        }

        foreach (var client in clients)
        {
            var isExisted = await dbContext.Clients.AnyAsync(x => x.ClientId == client.ClientId);
            if (!isExisted)
                await dbContext.Clients.AddAsync(client.ToEntity());
        }

        rows += await dbContext.SaveChangesAsync();
        return rows;
    }
}

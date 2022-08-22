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

            foreach (var resource in identityResources)
            {
                var entityInDb = await dbContext.IdentityResources.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Name == resource.Name);

                var entity = resource.ToEntity();

                entity.Id = entityInDb?.Id ?? 0;
                dbContext.Update(entity);
            }

            foreach (var apiScope in apiScopes)
            {
                var entityInDb = await dbContext.ApiScopes.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Name == apiScope.Name);

                var entity = apiScope.ToEntity();

                entity.Id = entityInDb?.Id ?? 0;
                dbContext.Update(entity);
            }

            foreach (var client in clients)
            {
                var entityInDb = await dbContext.Clients.AsNoTracking()
                    .SingleOrDefaultAsync(c => c.ClientId == client.ClientId);

                var entity = client.ToEntity();

                entity.Id = entityInDb?.Id ?? 0;
                dbContext.Update(entity);
            }

            await dbContext.SaveChangesAsync();

            return services;
        }
    }
}

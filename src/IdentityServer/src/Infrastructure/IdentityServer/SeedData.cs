﻿using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace DuongTruong.IdentityServer.Infrastructure.IdentityServer
{
    public static class SeedData
    {
        public static async Task<IServiceProvider> InitialConfigurationDataAsync<T>(this IServiceProvider services,
            IEnumerable<Duende.IdentityServer.Models.IdentityResource> identityResources,
            IEnumerable<Duende.IdentityServer.Models.ApiScope> apiScopes,
            IEnumerable<Duende.IdentityServer.Models.Client> clients)
            where T : DbContext, IConfigurationDbContext
        {
            var dbContext = services.GetRequiredService<T>();
            dbContext.Database.Migrate();

            foreach (var resource in identityResources)
            {
                var entityInDb = await dbContext.IdentityResources.SingleOrDefaultAsync(x => x.Name == resource.Name);

                var entity = resource.ToEntity();

                if (entityInDb != null)
                {
                    entity.Id = entityInDb.Id;
                    dbContext.Entry(entityInDb).CurrentValues.SetValues(entity);
                    dbContext.Update(entityInDb);
                }
                else
                    dbContext.Add(entity);
            }

            foreach (var apiScope in apiScopes)
            {
                var entityInDb = await dbContext.ApiScopes.SingleOrDefaultAsync(x => x.Name == apiScope.Name);

                var entity = apiScope.ToEntity();

                if (entityInDb != null)
                {
                    entity.Id = entityInDb.Id;
                    dbContext.Entry(entityInDb).CurrentValues.SetValues(entity);
                    dbContext.Update(entityInDb);
                }
                else
                    dbContext.Add(entity);
            }

            foreach (var client in clients)
            {
                var entityInDb = await dbContext.Clients.SingleOrDefaultAsync(c => c.ClientId == client.ClientId);

                var entity = client.ToEntity();

                if (entityInDb != null)
                {
                    entity.Id = entityInDb.Id;
                    dbContext.Entry(entityInDb).CurrentValues.SetValues(entity);
                    dbContext.Update(entityInDb);
                }
                else
                    dbContext.Add(entity);
            }

            await dbContext.SaveChangesAsync();

            return services;
        }
    }
}
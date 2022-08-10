﻿using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.UI.Configurations;

public static partial class IdentityServerConfigurations
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope(name: "api1", displayName: "MyAPI"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>()
        {
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
            
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                }
            },
        };
}

public static partial class IdentityServerConfigurations
{
    public static async Task InitialConfigurationData(ConfigurationDbContext dbContext)
    {

        dbContext.Database.Migrate();

        if (!await dbContext.Clients.AnyAsync())
        {
            foreach (var client in IdentityServerConfigurations.Clients)
            {
                dbContext.Clients.Add(client.ToEntity());
            }
            dbContext.SaveChanges();
        }


        if (!await dbContext.IdentityResources.AnyAsync())
        {
            foreach (var resource in IdentityServerConfigurations.IdentityResources)
            {
                dbContext.IdentityResources.Add(resource.ToEntity());
            }
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.ApiScopes.AnyAsync())
        {
            foreach (var resource in IdentityServerConfigurations.ApiScopes)
            {
                dbContext.ApiScopes.Add(resource.ToEntity());
            }
            await dbContext.SaveChangesAsync();
        }

        return;
    }
}
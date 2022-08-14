using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.IdentityServer;
using System.Diagnostics.CodeAnalysis;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static partial class BuilderExtenstions
    {
        [ExcludeFromCodeCoverage]
        public static IIdentityServerBuilder AddInMemoryConfigurationStore(this IIdentityServerBuilder builder)
        {
            return builder.AddInMemoryApiScopes(IdentityServerConfigurations.ApiScopes)
                .AddInMemoryClients(IdentityServerConfigurations.Clients)
                .AddInMemoryIdentityResources(IdentityServerConfigurations.IdentityResources);
        }

        public static async Task<IServiceProvider> SeedDataAsync(this IServiceProvider services)
        {
            await services.InitialConfigurationDataAsync<ConfigurationDbContext>(
                IdentityServerConfigurations.IdentityResources,
                IdentityServerConfigurations.ApiScopes,
                IdentityServerConfigurations.Clients);

            return services;
        }
    }
}

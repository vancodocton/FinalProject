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
    }
}

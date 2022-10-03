using System.Diagnostics.CodeAnalysis;

namespace DuongTruong.IdentityServer.UI.Configurations;

public static partial class BuilderExtenstions
{
    [ExcludeFromCodeCoverage]
    public static IIdentityServerBuilder AddInMemoryConfigurationStore(this IIdentityServerBuilder builder)
    {
        return builder.AddInMemoryIdentityResources(IdentityServerConfigurations.IdentityResources)
            .AddInMemoryApiScopes(IdentityServerConfigurations.ApiScopes)
            .AddInMemoryApiResources(IdentityServerConfigurations.ApiResources)
            .AddInMemoryClients(IdentityServerConfigurations.Clients);
    }
}

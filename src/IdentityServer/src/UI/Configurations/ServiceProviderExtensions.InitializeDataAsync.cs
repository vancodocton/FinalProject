using Duende.IdentityServer.EntityFramework.DbContexts;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static partial class ServiceProviderExtensions
    {
        public static async Task<IServiceProvider> InitializeDataAsync(this IServiceProvider serviceProvider)
        {
            using (var configurationDbContext = serviceProvider.GetService<ConfigurationDbContext>())
            {
                if (configurationDbContext != null)
                {
                    await IdentityServerConfigurations.InitialConfigurationData(configurationDbContext);
                }
            }

            return serviceProvider;
        }
    }
}

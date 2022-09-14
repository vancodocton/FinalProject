using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.IdentityServer;
using DuongTruong.IdentityServer.IntegratedTest.Fixtures;
using DuongTruong.IdentityServer.UI.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace DuongTruong.IdentityServer.IntegratedTest
{
    public class Test_DataSeeding : TestBase
    {
        public Test_DataSeeding(ITestOutputHelper outputHelper, IdentityServerFactory factory) : base(outputHelper, factory)
        {
        }

        [Fact]
        public async Task Test_SeedDataConfigurationDbAsync()
        {
            using (var serviceScope = factory.Services.CreateScope())
            {
                var sp = serviceScope.ServiceProvider;
                var db = sp.GetService<ConfigurationDbContext>();

                if (db != null)
                {
                    await db.InitialConfigurationDataAsync(
                        IdentityServerConfigurations.IdentityResources,
                        IdentityServerConfigurations.ApiScopes,
                        IdentityServerConfigurations.ApiResources,
                        IdentityServerConfigurations.Clients);

                    //var client = db.Clients.First();
                    //client.AllowOfflineAccess = !client.AllowOfflineAccess;

                    //db.SaveChanges();
                }
            }

            var client1 = factory.CreateDefaultClient();
        }
    }
}

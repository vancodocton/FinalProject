using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.IdentityServer;
using DuongTruong.IdentityServer.IntegratedTest.Fixtures;
using DuongTruong.IdentityServer.UI.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DuongTruong.IdentityServer.IntegratedTest
{
    public class Test_DataSeeding : IClassFixture<IdentityServerFixture>
    {
        private readonly WebApplicationFactory<UI.Program> factory;

        public Test_DataSeeding(IdentityServerFixture fixture)
        {
            factory = fixture.GetIdentityServer("Development");
        }

        [Fact]
        public async Task Test_SeedDataConfigurationDbAsync()
        {
            var client1 = factory.CreateDefaultClient();

            using (var serviceScope = factory.Server.Services.CreateScope())
            {
                var sp = serviceScope.ServiceProvider;
                var db = sp.GetService<ConfigurationDbContext>();

                if (db != null)
                {
                    await sp.InitialConfigurationDataAsync<ConfigurationDbContext>(
                        IdentityServerConfigurations.IdentityResources,
                        IdentityServerConfigurations.ApiScopes,
                        IdentityServerConfigurations.Clients);

                    //var client = db.Clients.First();
                    //client.AllowOfflineAccess = !client.AllowOfflineAccess;

                    //db.SaveChanges();
                }
            }
        }
    }
}

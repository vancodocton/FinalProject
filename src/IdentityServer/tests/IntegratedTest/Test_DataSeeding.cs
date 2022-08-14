using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.Infrastructure.IdentityServer;
using DuongTruong.IdentityServer.IntegratedTest.Fixtures;
using DuongTruong.IdentityServer.UI.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace DuongTruong.IdentityServer.IntegratedTest
{
    public class Test_DataSeeding : IClassFixture<WebAppsFixture>
    {
        private readonly WebApplicationFactory<UI.Program> factory;

        public Test_DataSeeding(WebAppsFixture fixture)
        {
            factory = fixture.GetIdentityServer("Development");
        }

        [Fact]
        public async Task Test_SeedDataConfigurationDbAsync()
        {
            var client1 = factory.CreateClient(IdentityServerFactory.DefaultClientOptions);
            var client2 = factory.CreateClient(IdentityServerFactory.DefaultClientOptions);

            var result1 = await client1.GetFromJsonAsync<object>("/.well-known/openid-configuration");
            var result2 = await client2.GetFromJsonAsync<object>("/.well-known/openid-configuration");

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

                    var client = db.Clients.First();
                    client.AllowOfflineAccess = !client.AllowOfflineAccess;                    
                    
                    db.SaveChanges();                    
                }
            }


            Assert.NotNull(result1);
            Assert.NotNull(result2);
        }
    }
}

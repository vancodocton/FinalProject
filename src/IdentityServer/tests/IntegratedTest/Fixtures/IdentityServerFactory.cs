using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures
{
    public class IdentityServerFactory : WebApplicationFactory<UI.Program>
    {
        private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

        private static bool _isDatabaseInitialized;
        public bool IsSharedDatabase { get; }

        public IdentityServerFactory(string? SharedDatabaseName = null) : base()
                    {
            ClientOptions.BaseAddress = new Uri("https://localhost:7011");
                }

        protected override IHost CreateHost(IHostBuilder builder)
                {
            builder.ConfigureHostConfiguration(config =>
                    {
                config.AddEnvironmentVariables("ASPNETCORE");
                config.AddInMemoryCollection(new Dictionary<string, string>()
                        {
                    ["ConnectionStrings:DockerPostgreSql"] = ConnectionStrings.DockerPostgreSql("IdentityServer-IntegrationTestDb"),
                        });
            })
                .UseEnvironment("Development");

            return base.CreateHost(builder);
                    }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();

                try
                {
                    semaphoreSlim.Wait();

                    using var scope = sp.CreateScope();
                    EnsureCreatedDatabase(sp);
                }
                catch
                    {
                    //throw;
                    }
                finally
                    {
                    semaphoreSlim.Release();
                    }
            });
                }

        public void EnsureCreatedDatabase(IServiceProvider sp)
        {
            if (!_isDatabaseInitialized)
            {
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                var applicationDbContext = scopedServices.GetService<ApplicationDbContext>();
                var configurationDbContext = scopedServices.GetService<ConfigurationDbContext>();
                var persistedGrantDbContext = scopedServices.GetService<PersistedGrantDbContext>();

                _ = applicationDbContext?.Database.EnsureDeleted();
                _ = configurationDbContext?.Database.EnsureDeleted();
                _ = persistedGrantDbContext?.Database.EnsureDeleted();

                applicationDbContext?.Database.Migrate();
                configurationDbContext?.Database.Migrate();
                persistedGrantDbContext?.Database.Migrate();

                _isDatabaseInitialized = true;
            }
        }
    }
}

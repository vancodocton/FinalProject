using Duende.IdentityServer.EntityFramework.DbContexts;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.UI.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures;

public class IdentityServerFactory : WebApplicationFactory<UI.Program>
{
    private readonly int _port = 7000;
    private readonly string _environmentName;

    private static readonly SemaphoreSlim _semaphoreWarmUp = new(1, 1);
    private static bool _isWarmUp;
    private static bool _isDatabaseInitialized;
    private static readonly IdentityServerFactory __warmupInstance = new();

    public IdentityServerFactory() : this(Environments.Development)
    {
    }

    private IdentityServerFactory(string environmentName) : base()
    {

        ClientOptions.BaseAddress = new Uri(string.Format("https://localhost:{0}", _port++));
        _environmentName = environmentName;

        try
        {
            _semaphoreWarmUp.Wait();
            if (!_isWarmUp && __warmupInstance is not null)
            {
                // avoid infinite loop while initializing __factory
                var __factory = __warmupInstance
                    .WithWebHostBuilder(config =>
                    {
                        config.UseSetting("WarmUp", WarmUpBehavior.Exit.ToString());

                        config.ConfigureServices(options =>
                        {
                            var serviceProvider = options.BuildServiceProvider();

                            var isDone = EnsureCreatedDatabase(serviceProvider);
                        });
                    });
                // The app is configured to exit after warmed up, exception is always throwns.
                __factory.CreateDefaultClient();
            }
        }
        catch
        {
            _isWarmUp = true;
            // throw;
        }
        finally
        {
            _semaphoreWarmUp.Release();
        }
    }


    private static bool EnsureCreatedDatabase(IServiceProvider sp)
    {
        if (!_isDatabaseInitialized)
        {
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var applicationDbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
            var configurationDbContext = scopedServices.GetRequiredService<ConfigurationDbContext>();
            var persistedGrantDbContext = scopedServices.GetRequiredService<PersistedGrantDbContext>();

            _ = applicationDbContext.Database.EnsureDeleted();
            _ = configurationDbContext.Database.EnsureDeleted();
            _ = persistedGrantDbContext.Database.EnsureDeleted();

            applicationDbContext.Database.MigrateAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            configurationDbContext.Database.MigrateAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            persistedGrantDbContext.Database.MigrateAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            _isDatabaseInitialized = true;
            return true;
        }
        return false;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environmentName);

        builder.ConfigureHostConfiguration((config) =>
        {
            config.AddEnvironmentVariables("ASPNETCORE");

            var inMemory = new Dictionary<string, string>()
            {
                ["ConnectionStrings:DockerPostgreSql"] = ConnectionStrings.DockerPostgreSql("IdentityServer-IntegrationTestDb"),
            };
            config.AddInMemoryCollection(inMemory);
        });

        var host = base.CreateHost(builder);
        return host;
    }
}

using DuongTruong.IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DuongTruong.IdentityServer.Infrastructure;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static class Dependencies
    {
        public static IServiceCollection AddDefaultDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionStrings["AspNetIdentity"],
                    options => options.MigrationsAssembly(MigrationAssemblyName.SqlServer));
            });

            services.AddCustomIdentity(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            var builder = services.AddIdentityServer(options =>
            {
            })
                .AddInMemoryConfigurationStore()
                .AddOperationalStore<ApplicationDbContext>(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        connectionStrings["AspNetIdentity"],
                        o => o.MigrationsAssembly(MigrationAssemblyName.SqlServer));
                })
                .AddAspNetIdentity<ApplicationUser>();

            return services;
        }

        public static IIdentityServerBuilder AddInMemoryConfigurationStore(this IIdentityServerBuilder builder)
        {
            return builder.AddInMemoryApiScopes(IdentityServerConfigurations.ApiScopes)
                .AddInMemoryClients(IdentityServerConfigurations.Clients)
                .AddInMemoryIdentityResources(IdentityServerConfigurations.IdentityResources);
        }
    }
}
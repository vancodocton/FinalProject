using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                connectionStrings["AspNetIdentity"],
                actions => actions.MigrationsAssembly(MigrationAssemblyName.SqlServer)));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();

            services.AddDefaultIdentityServer(configuration);

            return services;
        }

        public static IIdentityServerBuilder AddDefaultIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings");

            var builder = services.AddIdentityServer(configuration)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        connectionStrings["AspNetIdentity"],
                        o => o.MigrationsAssembly(MigrationAssemblyName.SqlServer));
                })
                .AddOperationalStore<ApplicationDbContext>(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        connectionStrings["AspNetIdentity"],
                        o => o.MigrationsAssembly(MigrationAssemblyName.SqlServer));
                })
                .AddAspNetIdentity<ApplicationUser>();

            return builder;
        }
    }
}
using DuongTruong.IdentityServer.Infrastructure;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static class Dependencies
    {
        static readonly bool isAddInMemoryConfigurationStore = false;

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
                .AddAspNetIdentity<ApplicationUser>()
                .AddOperationalStore<ApplicationDbContext>(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        connectionStrings["AspNetIdentity"],
                        o => o.MigrationsAssembly(MigrationAssemblyName.SqlServer));
                });

            if (isAddInMemoryConfigurationStore)
                builder.AddInMemoryConfigurationStore();
            else
                builder.AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        connectionStrings["AspNetIdentity"],
                        o => o.MigrationsAssembly(MigrationAssemblyName.SqlServer));
                });

            return services;
        }
    }
}
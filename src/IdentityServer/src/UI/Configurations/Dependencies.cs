using DuongTruong.IdentityServer.Infrastructure;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.Infrastructure.SqlServer;
using Microsoft.AspNetCore.Identity;

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
                options.UseDefaultSqlServer(connectionString: connectionStrings["AspNetIdentity"]);
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
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseDefaultSqlServer(
                        connectionString: connectionStrings["AspNetIdentity"]);
                });

            if (isAddInMemoryConfigurationStore)
                builder.AddInMemoryConfigurationStore();
            else
                builder.AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseDefaultSqlServer(
                        connectionString: connectionStrings["AspNetIdentity"]);
                });

            return services;
        }
    }
}
using DuongTruong.IdentityServer.Infrastructure;
using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.Infrastructure.PostgreSql;
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
                //options.UseDefaultSqlServer(connectionString: connectionStrings["DockerSqlServer"]);
                options.UseDefaultNpgsql(connectionString: connectionStrings["DockerPostgreSql"]);
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
                    options.ConfigureDbContext = b => b.UseDefaultNpgsql(
                        connectionString: connectionStrings["DockerPostgreSql"]);
                });

            if (isAddInMemoryConfigurationStore)
                builder.AddInMemoryConfigurationStore();
            else
                builder.AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseDefaultNpgsql(
                        connectionString: connectionStrings["DockerPostgreSql"]);
                });

            return services;
        }
    }
}

// dotnet ef migrations add -p ..\Infrastructure.PostgreSql\ -c PersistedGrantDbContext -o IdentityServer/PersistedGrantDb/Migrations CreatePersistedGrantDbSchema
using DuongTruong.IdentityServer.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultDependencies(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings");
            
            service.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                connectionStrings["AspNetIdentity"],
                actions => actions.MigrationsAssembly("DuongTruong.IdentityServer.Infrastructure.SqlServer.Migrations")));

            service.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();

            return service;
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DuongTruong.IdentityServer.Infrastructure.Identity
{
    public static class SeedData
    {
        public static async Task<IServiceProvider> InitialIdentityRolesAsync<T>(this IServiceProvider services,
            params string[] roleNames)
            where T : IdentityRole, new()
        {
            var roleManager = services.GetRequiredService<RoleManager<T>>();

            var logger = services.GetRequiredService<ILogger<WebApplication>>();

            foreach (var roleName in roleNames)
            {
                await roleManager.CreateAsync(new T { Name = roleName });
            }

            return services;
        }
    }
}

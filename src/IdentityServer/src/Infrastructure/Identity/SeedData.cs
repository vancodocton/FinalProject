using Microsoft.AspNetCore.Identity;

namespace DuongTruong.IdentityServer.Infrastructure.Identity;

public static class SeedData
{
    public static async Task InitialRolesAsync<T>(this RoleManager<T> roleManager,
        params string[] roleNames) where T : IdentityRole, new()
    {
        foreach (var roleName in roleNames)
        {
            await roleManager.CreateAsync(new T() { Name = roleName });
        }
    }
}

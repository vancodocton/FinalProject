using DuongTruong.IdentityServer.UI.Utils;
using System.Diagnostics;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static partial class BuilderExtenstions
    {
        public static async Task<IHost> WarmUpAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var stopwatch = scope.ServiceProvider.GetRequiredService<Stopwatch>();

            // seed data
            Program.SemaphoreSlim.Wait();
            _ = await scope.ServiceProvider.SeedDataAsync();
            Program.SemaphoreSlim.Release();

            return host;
        }
    }
}

using DuongTruong.IdentityServer.UI.Utils;
using System.Diagnostics;

namespace DuongTruong.IdentityServer.UI.Configurations
{
    public static partial class BuilderExtenstions
    {
        public static async Task<WebApplication> WarmUpAsync(this WebApplication app, WarmUpBehavior warmUpBehavior = WarmUpBehavior.Skip)
        {
            if (warmUpBehavior == WarmUpBehavior.Skip)
            {
                app.Logger.LogInformation("Skip warm up application.");
                return app;
            }

            using var scope = app.Services.CreateAsyncScope();

            var stopwatch = scope.ServiceProvider.GetRequiredService<Stopwatch>();

            // seed data
            Program.SemaphoreSlim.Wait();
            _ = await scope.ServiceProvider.SeedDataAsync();
            app.Logger.LogInformation("Finished seeding data while warming up.");
            Program.SemaphoreSlim.Release();

            app.Logger.LogInformation("Warming up consumes {time}s.", stopwatch.ElapsedMilliseconds / 1000.0);
            return app;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures
{
    public class WebAppsFixture
    {
        public WebAppsFixture()
        {
        }

        public WebApplicationFactory<UI.Program> GetIdentityServer(string environmentName)
        {
            return new IdentityServerFactory().WithWebHostBuilder(options =>
            {
                options.ConfigureAppConfiguration((context, builder) =>
                {
                    context.HostingEnvironment.EnvironmentName = environmentName;
                });
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures
{
    public class IdentityServerFixture
    {
        private static object _lock = new();

        private static Dictionary<string, IdentityServerFactory> _factories = new();

        public IdentityServerFixture()
        {
        }

        public WebApplicationFactory<UI.Program> GetIdentityServer(string environmentName)
        {
            lock (_lock)
            {
                if (_factories.GetValueOrDefault(environmentName) is null)
                {
                    var factory = new IdentityServerFactory(environmentName);

                    _factories.Add(environmentName, factory);
                };
            }

            return _factories[environmentName];
        }
    }
}

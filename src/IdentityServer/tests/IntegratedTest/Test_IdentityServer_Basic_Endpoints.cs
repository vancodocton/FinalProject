using DuongTruong.IdentityServer.IntegratedTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace DuongTruong.IdentityServer.IntegratedTest
{
    public class Test_IdentityServer_Basic_Endpoints : IClassFixture<WebAppsFixture>
    {
        private readonly WebAppsFixture fixture;

        private readonly WebApplicationFactory<UI.Program> identityServer;

        private readonly WebApplicationFactoryClientOptions identityServerClientOptions;
        public Test_IdentityServer_Basic_Endpoints(WebAppsFixture fixture)
        {
            this.fixture = fixture;
            identityServer = fixture.GetIdentityServer("Development");
            identityServerClientOptions = new WebApplicationFactoryClientOptions()
            {
                BaseAddress = new Uri("https://localhost:7011"),
            };
        }

        [Fact]
        public async Task Test_Discovery_Endpoint_Return_JsonDocument()
        {
            var client = identityServer.CreateClient(identityServerClientOptions);

            var json = await client.GetFromJsonAsync<object>("/.well-known/openid-configuration");

            Assert.NotNull(json);
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/Privacy")]
        [InlineData("/Error")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = identityServer.CreateClient(identityServerClientOptions);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType!.ToString());
        }
    }
}
using DuongTruong.IdentityServer.IntegratedTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace DuongTruong.IdentityServer.IntegratedTest
{
    public class Test_IdentityServer_Auth_Endpoints : IClassFixture<IdentityServerFixture>
    {
        private readonly WebApplicationFactory<UI.Program> factory;

        public Test_IdentityServer_Auth_Endpoints(IdentityServerFixture fixture)
        {
            factory = fixture.GetIdentityServer("Development");
        }

        [Fact]
        public async Task Test_Discovery_Endpoint_Return_JsonDocument()
        {
            var client = factory.CreateDefaultClient();

            var json = await client.GetFromJsonAsync<object>("/.well-known/openid-configuration");

            Assert.NotNull(json);
        }

        [Theory]
        [InlineData("/Identity/Account/Login")]
        [InlineData("/Identity/Account/Register")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = factory.CreateDefaultClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType!.ToString());
        }
    }
}
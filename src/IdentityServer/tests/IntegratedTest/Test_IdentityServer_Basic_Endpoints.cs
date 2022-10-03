using DuongTruong.IdentityServer.IntegratedTest.Fixtures;
using Xunit.Abstractions;

namespace DuongTruong.IdentityServer.IntegratedTest;

public class Test_IdentityServer_Basic_Endpoints : TestBase
{
    public Test_IdentityServer_Basic_Endpoints(
        IdentityServerFactory identityServerFactory,
        ITestOutputHelper outputHelper)
        : base(identityServerFactory, outputHelper)
    {
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Index")]
    [InlineData("/Privacy")]
    [InlineData("/Error")]
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

using Xunit.Abstractions;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures;

public class TestBase : IClassFixture<IdentityServerFactory>
{
    protected readonly ITestOutputHelper outputHelper;
    protected readonly IdentityServerFactory factory;

    public TestBase(IdentityServerFactory factory, ITestOutputHelper outputHelper)
    {
        this.outputHelper = outputHelper;
        this.factory = factory;
    }
}

using DuongTruong.IdentityServer.Core.Helpers;
using DuongTruong.IdentityServer.Core.Options;
using Xunit.Abstractions;

namespace DuongTruong.IdentityServer.UnitTest.IdentityServer;

public class TestApiResourceBuilder
{
    private const string roleClaim = RbacDefaults.RoleClaim;
    private readonly ITestOutputHelper output;

    public TestApiResourceBuilder(ITestOutputHelper output)
    {
        this.output = output;
        this.output.WriteLine($"Role type '{roleClaim}'.");
    }

    [Fact]
    public void ConstructorRbacApiResource_WithValidInput_ContainsRoleClaims()
    {
        var name = "name";
        var displayName = "displayName";
        var obj = new ApiResourceBuilder(name, displayName)
            .EnableRbac()
            .Build();

        Assert.Contains(roleClaim, obj.UserClaims);
    }

    [Fact]
    public void ConstructorRbacApiResource_WithUserClaims_Success()
    {
        var name = "name";
        var displayName = "displayName";

        var addidionalClaim = "additional_claim";

        var obj = new ApiResourceBuilder(name, displayName)
            .EnableRbac()
            .IncludeClaims(addidionalClaim)
            .Build();

        Assert.Contains(roleClaim, obj.UserClaims);
        Assert.Contains(addidionalClaim, obj.UserClaims);
    }

    [Fact]
    public void ConstructorRbacApiResource_WithScopes_Success()
    {
        var name = "name";
        var displayName = "displayName";

        var scope1 = "scope1";
        var scope2 = "scope2";

        var obj = new ApiResourceBuilder(name, displayName)
            .EnableRbac()
            .AllowScopes(scope1)
            .AllowScopes(scope2)
            .Build();

        Assert.Contains(roleClaim, obj.UserClaims);
        Assert.Contains(scope1, obj.Scopes);
        Assert.Contains(scope2, obj.Scopes);
    }

    [Fact]
    public void Test_WithDuplicateScope_ShouldHaveUniqueScopce()
    {
        var name = "name";
        var displayName = "displayName";

        var scope1 = "scope";
        var scope2 = "scope";

        var obj = new ApiResourceBuilder(name, displayName)
            .EnableRbac()
            .AllowScopes(scope1)
            .AllowScopes(scope2)
            .Build();

        Assert.Contains(roleClaim, obj.UserClaims);
        Assert.Contains("scope", obj.Scopes);
        Assert.Equal(1, obj.Scopes.Count);
    }

    [Fact]
    public void Test_WithDuplicateClaim_ShouldHaveUniqueClaim()
    {
        var name = "name";
        var displayName = "displayName";

        var obj = new ApiResourceBuilder(name, displayName)
            .EnableRbac()
            .IncludeClaims(roleClaim)
            .Build();

        Assert.Contains(roleClaim, obj.UserClaims);
        Assert.Equal(1, obj.UserClaims.Count);
    }
}

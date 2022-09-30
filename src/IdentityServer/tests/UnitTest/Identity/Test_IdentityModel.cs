using DuongTruong.IdentityServer.Infrastructure.Identity;

namespace DuongTruong.IdentityServer.UnitTest.Identity;

public class Test_IdentityModel
{
    [Fact]
    public void Test_User_Ctor_IdNotNull()
    {
        var user1 = new ApplicationUser(userName: "username");
        var user2 = new ApplicationUser();

        Assert.False(string.IsNullOrEmpty(user1.Id));
        Assert.Equal("username", user1.UserName);

        Assert.False(string.IsNullOrEmpty(user2.Id));
        Assert.Null(user2.UserName);
    }

    [Fact]
    public void Test_Role_Ctor_IdNotNull()
    {
        var role1 = new ApplicationRole(roleName: "role name");
        var role2 = new ApplicationRole();

        Assert.False(string.IsNullOrEmpty(role1.Id));
        Assert.False(string.IsNullOrEmpty(role2.Id));
    }
}

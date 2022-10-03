namespace DuongTruong.IdentityServer.UI.Utils;

public class Role
{
    public const string Default = "User";

    public const string Admin = "Admin";

    public static IReadOnlyList<string> ToList() => new List<string>()
    {
        Default,
        Admin,
    };

    public static string[] ToArray() => ToList().ToArray();
}

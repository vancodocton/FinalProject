namespace DuongTruong.IdentityServer.Infrastructure.PostgreSql;

public class Assembly
{
    public static string Name =>
        typeof(Assembly).Assembly.GetName().Name
        ?? throw new ArgumentNullException(nameof(Name));
}

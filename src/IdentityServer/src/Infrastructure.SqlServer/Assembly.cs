namespace DuongTruong.IdentityServer.Infrastructure.SqlServer
{
    public class Assembly
    {
        public static string Name =>
            typeof(Assembly).Assembly.GetName().Name
            ?? throw new ArgumentNullException(nameof(Name));
    }
}
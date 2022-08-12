namespace DuongTruong.IdentityServer.Infrastructure
{
    public partial class MigrationAssemblyName
    {
        public static string SqlServer =>
            typeof(MigrationAssemblyName).Assembly.GetName().Name
            ?? throw new ArgumentNullException(nameof(SqlServer));
    }
}
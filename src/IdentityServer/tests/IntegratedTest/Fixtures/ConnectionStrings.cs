using System.Diagnostics.CodeAnalysis;

namespace DuongTruong.IdentityServer.IntegratedTest.Fixtures;

[ExcludeFromCodeCoverage]
public static class ConnectionStrings
{
    public static string SqlServerLocalDb(string databaseName)
        => string.Format(format: "Server=(localdb)\\mssqllocaldb;Database={0};Trusted_Connection=True;MultipleActiveResultSets=true", arg0: databaseName);
    //= "Server=localhost,1433;Database={0};User ID=sa;Password=ba9b0b16-47a0-4dfe-be28-8e2707c8fee1;Persist Security Info=False;";
    public static string DockerPostgreSql(string databaseName)
        //=> string.Format(format: "Server=localhost;Username=postgres;Database={0};Pooling=true", arg0: databaseName);
        => string.Format(format: "Host=localhost;Database={0};Port=5432;Username=postgres;Password=postgres;Include Error Detail=true;Pooling=true", arg0: databaseName);

}

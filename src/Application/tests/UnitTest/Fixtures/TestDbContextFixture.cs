using System.Data.Common;
using System.Data.SqlClient;
using DuongTruong.Application.UnitTest.Fixtures.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DuongTruong.Application.UnitTest.Fixtures;
public class TestDbContextFixture
{
    public readonly string ConnectionStringDocker = ConnectionStrings.DockerPostgreSql("DuongTruong.Application.UnitTest");

    private static readonly object _lock = new object();
    private static bool _databaseInitialized;

    public TestDbContextFixture()
    {
        EnsureInitialized();
    }

    public DbConnection CreateConnection() => new NpgsqlConnection(ConnectionStringDocker);

    public TestDbContext CreateContext(bool useTransaction = false)
    {
        var context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
            .UseNpgsql(ConnectionStringDocker).Options);

        if (useTransaction)
        {
            context.Database.BeginTransaction();
        }

        return context;
    }

    private void EnsureInitialized()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }

                _databaseInitialized = true;
            }
        }
    }
}

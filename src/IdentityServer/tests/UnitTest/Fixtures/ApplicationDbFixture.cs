using DuongTruong.IdentityServer.Infrastructure.Identity;
using DuongTruong.IdentityServer.Infrastructure.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.UnitTest.Fixtures
{
    public class ApplicationDbFixture : TestDatabaseBase<ApplicationDbContext>
    {
        public override DbContextOptions<ApplicationDbContext> Options { get; protected set; }
            = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseDefaultNpgsql(ConnectionStrings.DockerPostgreSql("IdentityServer-UnitTest"))
            .Options;

        private static bool _isDatabaseInitialized = false;
        private static object _lock = false;

        public ApplicationDbFixture()
        {
            EnsureDatabaseCreated(ref _lock, ref _isDatabaseInitialized);
        }

        public override ApplicationDbContext CreateContext()
        {
            return new ApplicationDbContext(Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.UnitTest.Fixtures;

public abstract class TestDatabaseBase<T> where T : DbContext
{
    public abstract DbContextOptions<T> Options { get; protected set; }

    public abstract T CreateContext();

    protected virtual void EnsureDatabaseCreated(ref object _lock, ref bool isDatabaseInitialized)
    {
        lock (_lock)
        {
            if (!isDatabaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();

                    if (context.Database.IsRelational())
                        context.Database.Migrate();
                    else
                        context.Database.EnsureCreated();

                    context.SaveChanges();
                }

                isDatabaseInitialized = true;
            }
        }
    }
}

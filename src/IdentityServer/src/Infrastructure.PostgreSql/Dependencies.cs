using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.Infrastructure.PostgreSql;

public static class Dependencies
{
    public static DbContextOptionsBuilder<T> UseDefaultNpgsql<T>(
        this DbContextOptionsBuilder<T> optionsBuilder,
        string connectionString) where T : DbContext
    {
        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.MigrationsAssembly(Assembly.Name);
        });

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder UseDefaultNpgsql(
        this DbContextOptionsBuilder optionsBuilder,
        string connectionString)
    {
        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.MigrationsAssembly(Assembly.Name);
        });

        return optionsBuilder;
    }
}

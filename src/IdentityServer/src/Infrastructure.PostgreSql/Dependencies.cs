using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.Infrastructure.PostgreSql
{
    public static class Dependencies
    {
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
}

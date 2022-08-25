using Microsoft.EntityFrameworkCore;

namespace DuongTruong.IdentityServer.Infrastructure.SqlServer
{
    public static class Dependencies
    {
        public static DbContextOptionsBuilder UseDefaultSqlServer(
            this DbContextOptionsBuilder optionsBuilder,
            string connectionString)
        {

            optionsBuilder.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly(Assembly.Name);
                });

            return optionsBuilder;
        }
    }
}

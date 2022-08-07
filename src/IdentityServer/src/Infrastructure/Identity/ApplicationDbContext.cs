using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DuongTruong.IdentityServer.Infrastructure.Identity
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, ApplicationRole, string>,
        Duende.IdentityServer.EntityFramework.Interfaces.IPersistedGrantDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = null!;
        public DbSet<Key> Keys { get; set; } = null!;
        public DbSet<ServerSideSession> ServerSideSessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var options = this.GetService<Duende.IdentityServer.EntityFramework.Options.OperationalStoreOptions>();
            builder.ConfigurePersistedGrantContext(options);
        }
    }
}
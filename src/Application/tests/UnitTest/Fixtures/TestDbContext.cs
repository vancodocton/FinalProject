using DuongTruong.Application.UnitTest.Fixtures.Entities;
using DuongTruong.Application.UnitTest.Fixtures.Entities.Seeds;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.Application.UnitTest.Fixtures;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(DataSeed.Users);
    }
}

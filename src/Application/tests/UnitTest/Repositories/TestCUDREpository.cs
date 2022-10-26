using DuongTruong.Application.Infrastructure.Repositories;
using DuongTruong.Application.UnitTest.Fixtures;
using DuongTruong.Application.UnitTest.Fixtures.Entities;
using DuongTruong.Application.UnitTest.Fixtures.Entities.Seeds;

namespace DuongTruong.Application.UnitTest.Repositories;

public class TestCUDREpository : IClassFixture<TestDbContextFixture>
{
    private readonly TestDbContextFixture fixture;

    public TestCUDREpository(TestDbContextFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GivenValidDto_CreateEntity_SuccessAsync()
    {
        var dbcontext = fixture.CreateContext(useTransaction: true);
        var repo = new EfRepository<User>(dbcontext);

        var newUser = new User("Nguyen Ky Duong Truong", 18);

        Assert.Equal(Guid.Empty, newUser.Id);

        await repo.Create(newUser);
        await repo.SaveChangesAsync();

        Assert.NotEqual(Guid.Empty, newUser.Id);

        dbcontext.Database.CommitTransaction();
    }


    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GivenExistedId_RemoveEntity_SuccessAsync(int index)
    {
        var dbcontext = fixture.CreateContext(useTransaction: true);
        var repo = new EfRepository<User>(dbcontext);

        var removedUser = new User(DataSeed.Users[index].Id);
        await repo.Remove(removedUser);
        await repo.SaveChangesAsync();

        Assert.Null(await repo.FindByIdAsync(removedUser.Id));

        dbcontext.Database.RollbackTransaction();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GivenExistedId_ReadThenUpdateEntity_SuccessAsync(int index)
    {
        var dbcontext = fixture.CreateContext(useTransaction: true);
        var repo = new EfRepository<User>(dbcontext);

        var updatedUser = await repo.FindByIdAsync(DataSeed.Users[index].Id);

        if (updatedUser == null)
        {
            Assert.NotNull(updatedUser);
            return;
        }

        updatedUser.Age = 20;
        var row = await repo.SaveChangesAsync();

        Assert.Equal(1, row);

        dbcontext.Database.RollbackTransaction();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GivenExistedId_UpdateWithoutReadEntity_SuccessAsync(int index)
    {
        var dbcontext = fixture.CreateContext(useTransaction: true);
        var repo = new EfRepository<User>(dbcontext);
        dbcontext.ChangeTracker.Clear();

        var untractedOldUser = DataSeed.Users[index];
        var updatedUser = new User(untractedOldUser.Id, "New name", untractedOldUser.Age + 100);
        await repo.Update(updatedUser);
        var row = await repo.SaveChangesAsync();

        Assert.Equal(1, row);
        Assert.NotEqual(untractedOldUser.Age, updatedUser.Age);

        dbcontext.Database.RollbackTransaction();
    }
}

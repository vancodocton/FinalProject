using DuongTruong.Application.Infrastructure.Repositories;
using DuongTruong.Application.UnitTest.Fixtures;
using DuongTruong.Application.UnitTest.Fixtures.Entities;
using DuongTruong.Application.UnitTest.Fixtures.Entities.Seeds;
using DuongTruong.Application.UnitTest.Fixtures.Specs;
using DuongTruong.SharedKernel;

namespace DuongTruong.Application.UnitTest.Repositories;

public class TestReadRepository : IClassFixture<TestDbContextFixture>
{
    private readonly TestDbContextFixture fixture;

    public TestReadRepository(TestDbContextFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GivenExistId_ReturnEntity()
    {
        var userRepository = new EfRepository<User>(fixture.CreateContext());

        var u = await userRepository.FindByIdAsync(DataSeed.Users[1].Id);

        Assert.NotNull(u);
        Assert.Equal(DataSeed.Users[1].FullName, u?.FullName);
    }

    [Fact]
    public async Task GivenNonExistId_ReturnNull()
    {
        var userRepository = new EfRepository<User>(fixture.CreateContext());

        var u = await userRepository.FindByIdAsync(Guid.Empty);

        Assert.Null(u);
    }


    [Fact]
    public async Task GivenSpecification_Find_ReturnEntity()
    {
        var userRepository = new EfRepository<User>(fixture.CreateContext());

        ISpecification<User> spec = new UserWithIdSpec(DataSeed.Users[2].Id);
        var u = await userRepository.FindBySpecAsync(spec);

        Assert.NotNull(u);
        Assert.Equal(DataSeed.Users[2].FullName, u?.FullName);
    }

    [Fact]
    public async Task GivenListableSpecification_Query_ReturnEntity()
    {
        var userRepository = new EfRepository<User>(fixture.CreateContext());

        ISpecification<User> spec = new UserWithIdSpec(DataSeed.Users[3].Id);
        var user = await userRepository.FindBySpecAsync(spec);

        var users = await userRepository.GetAllBySpecAsync(spec);

        Assert.NotNull(user);
        Assert.NotEmpty(users);
        Assert.Equal(DataSeed.Users[3].FullName, user?.FullName);
    }

    [Fact]
    public async Task GivenUnlistableSpecification_Query_ReturnNull()
    {
        var userRepository = new EfRepository<User>(fixture.CreateContext());

        ISpecification<User> spec = new UserWithIdSpec(Guid.NewGuid());
        var user = await userRepository.FindBySpecAsync(spec);
        var users = await userRepository.GetAllBySpecAsync(spec);

        Assert.Null(user);
        Assert.Empty(users);
    }

    [Fact]
    public async Task GivenDataNotEmpty_Query_ReturnAll()
    {
        var userRepository = new EfRepository<User>(fixture.CreateContext());

        var users = await userRepository.GetAllAsync();

        Assert.NotEmpty(users);
    }
}

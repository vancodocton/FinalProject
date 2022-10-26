using DuongTruong.Application.UnitTest.Fixtures.Entities.Seeds;
using DuongTruong.Application.UnitTest.Fixtures.Specs;

namespace DuongTruong.Application.UnitTest;

public class TestSpecification
{
    [Fact]
    public void GivenConition_CreateSpec_ShouldWork()
    {
        var spec = new UserWithIdSpec(new Guid());

        Assert.Null(DataSeed.Users.FirstOrDefault(u => spec.IsSatisfiedBy(u)));
    }
}

using DuongTruong.Application.UnitTest.Fixtures.Entities;
using DuongTruong.SharedKernel;

namespace DuongTruong.Application.UnitTest.Fixtures.Specs;

public class UserWithIdSpec : SpecificationBase<User>, ISpecification<User>
{
    public UserWithIdSpec(Guid id)
    {
        FilterExpression = u => u.Id == id;
    }
}

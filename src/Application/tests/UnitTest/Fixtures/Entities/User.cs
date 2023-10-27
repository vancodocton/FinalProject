using DuongTruong.SharedKernel;

namespace DuongTruong.Application.UnitTest.Fixtures.Entities;

public class User : EntityBase
{
    public string FullName { get; set; }

    public int Age { get; set; }

    public User(string fullName, int age) : this(Guid.Empty, fullName, age)
    { }

    public User(Guid id, string fullName, int age) : base(id)
    {
        FullName = fullName;
        Age = age;
    }

    public User(Guid id) : base(id)
    {
        FullName = null!;
    }
}

namespace DuongTruong.Application.UnitTest.Fixtures.Entities.Seeds;

internal class DataSeed
{
    public static IList<User> Users = new List<User>()
    {
        new User(Guid.NewGuid(),
            "Nguyen Ky", 11),
        new User(Guid.NewGuid(),
            "Nguyen Duong",12),
        new User(Guid.NewGuid(),
            "Nguyen Truong",10),
        new User(Guid.NewGuid(),
            "Duong Truong",14)
    };
}

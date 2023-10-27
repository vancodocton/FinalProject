namespace DuongTruong.SharedKernel;

public class EntityBase : IEntity<Guid>
{
    public Guid Id { get; protected set; }

    public EntityBase(Guid id)
    {
        Id = id;
    }

    public EntityBase() : this(Guid.Empty) { }
}

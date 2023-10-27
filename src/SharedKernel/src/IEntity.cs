namespace DuongTruong.SharedKernel;

public interface IEntity<TKey>
{
    public TKey Id { get; }
}

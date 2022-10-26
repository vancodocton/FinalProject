namespace DuongTruong.SharedKernel;

public interface IRepository<TEntity> : IReadRepository<TEntity>
{
    public bool IsAutoSaveChanges { get; }

    public Task<TEntity> Create(TEntity entity);

    public Task<TEntity> Remove(TEntity entity);

    public Task<TEntity> Update(TEntity entity);

    public Task<int> SaveChangesAsync();
}

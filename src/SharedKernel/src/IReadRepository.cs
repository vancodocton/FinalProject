namespace DuongTruong.SharedKernel;

public interface IReadRepository<TEntity>
{
    public ValueTask<TEntity?> FindByIdAsync<TKey>(TKey id);

    public Task<TEntity?> FindBySpecAsync(ISpecification<TEntity> specification);

    public Task<List<TEntity>> GetAllAsync();

    public Task<List<TEntity>> GetAllBySpecAsync(ISpecification<TEntity> specification);
}

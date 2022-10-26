using DuongTruong.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace DuongTruong.Application.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly DbContext dbContext;
    protected readonly DbSet<T> dbSet;

    public EfRepository(DbContext dbContext, bool isAutoSaveChanges = false)
    {
        this.dbContext = dbContext;
        this.isAutoSaveChanges = isAutoSaveChanges;
        this.dbSet = dbContext.Set<T>();
    }

    public bool IsAutoSaveChanges { get => isAutoSaveChanges; }

    protected bool isAutoSaveChanges;

    protected IQueryable<T> ApplySpecification(IQueryable<T> query, ISpecification<T> specification)
    {
        if (specification.FilterExpression is not null)
        {
            query = query.Where(specification.FilterExpression);
        }

        return query;
    }

    public ValueTask<T?> FindByIdAsync<TKey>(TKey id)
    {
        return dbSet.FindAsync(id);
    }

    public Task<T?> FindBySpecAsync(ISpecification<T> specification)
    {
        return ApplySpecification(dbSet.AsQueryable(), specification)
            .FirstOrDefaultAsync();
    }

    public Task<List<T>> GetAllAsync()
    {
        return dbSet.ToListAsync();
    }

    public Task<List<T>> GetAllBySpecAsync(ISpecification<T> specification)
    {
        return ApplySpecification(dbSet.AsQueryable(), specification)
            .ToListAsync();
    }

    public async Task<T> Create(T entity)
    {
        dbSet.Add(entity);
        if (IsAutoSaveChanges) _ = await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Remove(T entity)
    {
        dbSet.Remove(entity);
        if (IsAutoSaveChanges) _ = await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Update(T entity)
    {
        dbSet.Update(entity);
        if (IsAutoSaveChanges) _ = await dbContext.SaveChangesAsync();

        return entity;
    }

    public Task<int> SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }

}

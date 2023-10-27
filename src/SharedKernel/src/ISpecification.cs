using System.Linq.Expressions;

namespace DuongTruong.SharedKernel;

public interface ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? FilterExpression { get; }

    public bool IsSatisfiedBy(TEntity entity);
}

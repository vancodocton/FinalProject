using System.Linq.Expressions;

namespace DuongTruong.SharedKernel;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? FilterExpression { get; protected set; }

    public bool IsSatisfiedBy(T entity)
    {
        if (FilterExpression is null)
            return true;

        return FilterExpression.Compile()(entity);
    }
}

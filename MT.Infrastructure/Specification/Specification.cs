using System.Linq.Expressions;
using MT.Infrastructure.Specification.Interfaces;

namespace MT.Infrastructure.Specification;

public abstract class Specification<T>: ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();
    
    public Specification<T> And(Specification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }
}
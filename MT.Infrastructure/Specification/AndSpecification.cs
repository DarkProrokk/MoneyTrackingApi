using System.Linq.Expressions;

namespace MT.Infrastructure.Specification;

public class AndSpecification<T>(Specification<T> left, Specification<T> right): Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExp = left.ToExpression();
        var rightExp = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T));

        var andExpression = Expression.AndAlso(
            Expression.Invoke(leftExp, parameter),
            Expression.Invoke(rightExp, parameter)
        );

        return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
    }
}
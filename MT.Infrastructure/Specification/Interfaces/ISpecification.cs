using System.Linq.Expressions;

namespace MT.Infrastructure.Specification.Interfaces;

public interface ISpecification<T>
{
  
    Expression<Func<T, bool>> ToExpression();
}
using System.Linq.Expressions;
using MT.Infrastructure.Specification;

namespace Tests.Repository.Base;

public class NameSpecification(string name): Specification<TestEntity>
{
    public override Expression<Func<TestEntity, bool>> ToExpression()
    {
        return entity => entity.Name == name;
    }
}
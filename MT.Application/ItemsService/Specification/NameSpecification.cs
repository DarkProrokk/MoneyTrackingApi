using System.Linq.Expressions;
using MT.Domain.Entity;
using MT.Infrastructure.Specification;

namespace MT.Application.ItemsService.Specification;

public class NameSpecification(string name): Specification<Item>
{
    public override Expression<Func<Item, bool>> ToExpression()
    {
        return item => item.Name == name;
    }
}
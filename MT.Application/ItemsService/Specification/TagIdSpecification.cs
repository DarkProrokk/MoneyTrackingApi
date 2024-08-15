using System.Linq.Expressions;
using MT.Domain.Entity;
using MT.Infrastructure.Specification;

namespace MT.Application.ItemsService.Specification;

public class TagIdSpecification(Guid id): Specification<Item>
{
    public override Expression<Func<Item, bool>> ToExpression()
    {
        return items => items.Tags.Any(tag => tag.TagId == id);
    }
}
using System.Linq.Expressions;
using MT.Domain.Entity;
using MT.Infrastructure.Specification;

namespace MT.Application.ItemsService.Specification;

public class UserSpecification(User user): Specification<Item>
{
    public override Expression<Func<Item, bool>> ToExpression()
    {
        return items => items.User == user;
    }
}
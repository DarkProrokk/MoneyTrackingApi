using System.Linq.Expressions;
using MT.Domain.Entity;
using MT.Infrastructure.Specification;

namespace MT.Application.TagsService.Specification;

public class UserGuidSpecification(Guid guid): Specification<Tag>
{
    public override Expression<Func<Tag, bool>> ToExpression()
    {
        return tag => tag.UserId == guid;
    }
}
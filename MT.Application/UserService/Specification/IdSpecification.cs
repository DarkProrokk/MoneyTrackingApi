using System.Linq.Expressions;
using MT.Domain.Entity;
using MT.Infrastructure.Specification;

namespace MT.Application.UserService.Specification;

public class IdSpecification(Guid id): Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Id == id;
    }
}
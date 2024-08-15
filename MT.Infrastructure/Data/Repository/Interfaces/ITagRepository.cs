using MT.Domain.Entity;

namespace MT.Infrastructure.Data.Repository.Interfaces;

public interface ITagRepository: IBaseRepository<Tag>
{
    ICollection<Tag> GetExistingByGuids(IList<Guid> guids);
}
using MT.Domain.Entity;
using MT.Infrastructure.Data.Context;
using MT.Infrastructure.Data.Repository.Interfaces;

namespace MT.Infrastructure.Data.Repository;

public class TagRepository(TrackingContext context) : BaseRepository<Tag>(context), ITagRepository
{
    public ICollection<Tag> GetExistingByGuids(IList<Guid> guids)
    {
        return context.Tags.Where(tags => guids.Contains(tags.TagId)).ToHashSet();
    }

}
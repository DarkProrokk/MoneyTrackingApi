using MT.Application.BaseService.Interfaces;
using MT.Application.TagsService.Models;
using MT.Domain.Entity;

namespace MT.Application.TagsService.Interfaces;

public interface ITagsService: IBaseService<Tag>
{
    
    Task AddAsync(Tag tag, Guid userId);

    ICollection<Tag> GetByGuids(List<Guid> guids);

    IEnumerable<TagsDto> GetByUser(User user);

    IEnumerable<TagsDto> GetByUserGuid(Guid guid);
    
    Task AddWithUserAsync(Tag dto, User userId);
}
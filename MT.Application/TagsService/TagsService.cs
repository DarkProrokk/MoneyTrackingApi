using MT.Application.BaseService;
using MT.Domain.Entity;
using MT.Application.TagsService.Interfaces;
using MT.Application.TagsService.Mappers;
using MT.Application.TagsService.Models;
using MT.Application.TagsService.Specification;
using MT.Infrastructure.Data.Repository.Interfaces;

namespace MT.Application.TagsService;

public class TagsService(ITagRepository repository): BaseService<Tag>(repository), ITagsService
{
    public async Task AddAsync(Tag tag, Guid userId)
    {
        tag.UserId = userId;
        await repository.AddAsync(tag);
        await repository.SaveAsync();
    }

    public async Task AddWithUserAsync(Tag tag, User user)
    {
        tag.User = user;
        await repository.AddAsync(tag);
        await repository.SaveAsync();
    }

    public ICollection<Tag> GetByGuids(List<Guid> guids)
    {
        return repository.GetExistingByGuids(guids);
    }

    public IEnumerable<TagsDto> GetByUserGuid(Guid guid)
    {
        var userSpec = new UserGuidSpecification(guid);
        var mapper = BaseMapper.TagMapper();
        return repository.GetFilteredEntities(mapper,userSpec);
    }

    public IEnumerable<TagsDto> GetByUser(User user)
    {
        var userSpec = new UserSpecification(user);
        var mapper = BaseMapper.TagMapper();
        return repository.GetFilteredEntities(mapper, userSpec);
    }
}
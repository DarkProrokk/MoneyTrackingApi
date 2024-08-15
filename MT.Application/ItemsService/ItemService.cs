using MT.Application.BaseService;
using MT.Application.ItemsService.Interfaces;
using MT.Application.ItemsService.Models;
using MT.Application.ItemsService.Specification;
using MT.Domain.Entity;
using MT.Application.TagsService.Interfaces;
using MT.Infrastructure.Data.Repository.Interfaces;
using static MT.Application.ItemsService.Mappers.DtoMapper;

namespace MT.Application.ItemsService;

public class ItemService(IItemRepository itemRepository, ITagsService tagsService): BaseService<Item>(itemRepository), IItemService
{
    #region Get

    public IEnumerable<ItemsListDto> GetAllItems()
    {
        var dtoMapper = ItemToDtoMapper();
        var items = itemRepository.GetFilteredEntities(dtoMapper);
        return items;
    }
    
    public IEnumerable<ItemsListDto> GetItemsByTagId(Guid tagId)
    {
        var tagIdSpec = new TagIdSpecification(tagId);
        var dtoMapper = ItemToDtoMapper();
        var items = itemRepository.GetFilteredEntities(dtoMapper, tagIdSpec);
        return items;
    }

    public IEnumerable<ItemsListDto> GetItemsByUserId(Guid userId)
    {
        var userIdSpec = new UserIdSpecification(userId);
        var dtoMapper = ItemToDtoMapper();
        var items = itemRepository.GetFilteredEntities(dtoMapper, userIdSpec);
        return items;
    }
    
    public IEnumerable<ItemsListDto> GetItemsByUser(User user)
    {
        var userSpec = new UserSpecification(user);
        var dtoMapper = ItemToDtoMapper();
        var items = itemRepository.GetFilteredEntities(dtoMapper, userSpec);
        return items;
    }

    #endregion

    #region Add

    public async Task AddWithUserIdAsync(ItemsAddDto dto, Guid userId)
    {
        var item = AddDtoToItemMapper(dto);
        item.UserId = userId;
        var tags = tagsService.GetByGuids(dto.TagsId);
        item.Tags = tags;
        await itemRepository.AddAsync(item);
        await itemRepository.SaveAsync();
    }

    public async Task AddWithUserAsync(ItemsAddDto dto, User user)
    {
        var item = AddDtoToItemMapper(dto);
        item.User = user;
        var tags = tagsService.GetByGuids(dto.TagsId);
        item.Tags = tags;
        await itemRepository.AddAsync(item);
        await itemRepository.SaveAsync();
    }
    #endregion
    
}
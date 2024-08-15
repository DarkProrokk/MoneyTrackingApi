using MT.Application.BaseService.Interfaces;
using MT.Application.ItemsService.Models;
using MT.Domain.Entity;

namespace MT.Application.ItemsService.Interfaces;

public interface IItemService: IBaseService<Item>
{
    IEnumerable<ItemsListDto> GetAllItems();
    
    IEnumerable<ItemsListDto> GetItemsByTagId(Guid tagId);
    
    IEnumerable<ItemsListDto> GetItemsByUserId(Guid userId);

    IEnumerable<ItemsListDto> GetItemsByUser(User user);
    
    Task AddWithUserIdAsync(ItemsAddDto item, Guid id);

    Task AddWithUserAsync(ItemsAddDto item, User user);

    // IEnumerable<ItemsListDto> GetItemByName(string name, bool isUsefull);
}
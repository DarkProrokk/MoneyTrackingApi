using System.Linq.Expressions;
using MT.Application.ItemsService.Models;
using MT.Domain.Entity;

namespace MT.Application.ItemsService.Mappers;

public static class DtoMapper
{
    public static Expression<Func<Item, ItemsListDto>>? ItemToDtoMapper()
    {
        return item => new ItemsListDto
        {
            ItemId = item.ItemId,
            Name = item.Name,
            Price = item.Price,
            PossiblePrice = item.PossiblePrice,
            PossibleUseful = item.PossibleUseful,
            PriceDelta = item.PossiblePrice == null
                ? 0
                : (item.PossiblePrice - item.Price),
            Usefull = item.Useful,
            Description = item.Description,
            Date = item.Date,
            TagsId = item.Tags.Select(tag => tag.TagId).ToList(),
            User = item.User.Id
        };
    }

    public static Item AddDtoToItemMapper(ItemsAddDto dto)
    {
        return new Item
        {
            Name = dto.Name,
            Price = dto.Price,
            PossiblePrice = dto.PossiblePrice,
            PossibleUseful = dto.PossibleUseful,
            Description = dto.Description,
            Date = dto.Date,
            Useful = dto.Usefull
        };
    }
}
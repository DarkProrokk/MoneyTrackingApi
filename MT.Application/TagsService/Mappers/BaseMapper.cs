using System.Linq.Expressions;
using MT.Application.TagsService.Models;
using MT.Domain.Entity;

namespace MT.Application.TagsService.Mappers;

public static class BaseMapper
{
    public static Expression<Func<Tag, TagsDto>>? TagMapper()
    {
        return tag => new TagsDto
        {
            Id = tag.TagId,
            Name = tag.Name
        };
    }
}
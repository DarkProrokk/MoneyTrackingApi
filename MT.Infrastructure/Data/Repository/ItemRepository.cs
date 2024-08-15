using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MT.Domain.Entity;
using MT.Infrastructure.Data.Context;
using MT.Infrastructure.Data.Repository.Interfaces;

namespace MT.Infrastructure.Data.Repository;

public class ItemRepository(TrackingContext context): BaseRepository<Item>(context), IItemRepository
{
}
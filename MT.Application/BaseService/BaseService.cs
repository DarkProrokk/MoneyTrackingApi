using MT.Application.BaseService.Interfaces;
using MT.Infrastructure.Data.Repository;
using MT.Infrastructure.Data.Repository.Interfaces;

namespace MT.Application.BaseService;

public class BaseService<T>(IBaseRepository<T> repository): IBaseService<T> where T: class
{
    public async Task AddAsync(T entity)
    {
        await repository.AddAsync(entity);
    }

    public async Task<T?> GetByGuidAsync(Guid guid)
    {
        var entity = await repository.GetByGuidAsync(guid);
        return entity;
    }
    
    public async Task<bool> IsExistAsync(Guid guid)
    {
        var entity = await GetByGuidAsync(guid);
        return entity != null;
    }
    
    public IEnumerable<T> GetAll()
    {
        return repository.GetFilteredEntities<T, T>();
    }
}
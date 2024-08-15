namespace MT.Application.BaseService.Interfaces;

public interface IBaseService<T>
{
    public Task AddAsync(T entity);

    public Task<bool> IsExistAsync(Guid guid);
    public Task<T?> GetByGuidAsync(Guid guid);

    public IEnumerable<T> GetAll();
}
using System.Linq.Expressions;
using MT.Infrastructure.Specification.Interfaces;

namespace MT.Infrastructure.Data.Repository.Interfaces;

public interface IBaseRepository<T> where T: class
{

    public IEnumerable<TResult> GetFilteredEntities<TEntity, TResult>(Expression<Func<TEntity, TResult>>? mapper = null,
        ISpecification<TEntity>? specification = null) where TEntity : class;
    Task AddAsync(T entity);
    
    Task<T?> GetByGuidAsync(Guid guid);
    Task SaveAsync();

  

    //Async Method
    public Task Delete(Guid id);

    public void Delete(T entity);

    public void Update(T entity);
}
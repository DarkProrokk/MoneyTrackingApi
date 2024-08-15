using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MT.Domain.Entity;
using MT.Infrastructure.Data.Repository.Interfaces;
using MT.Infrastructure.Specification.Interfaces;

namespace MT.Infrastructure.Data.Repository;

public class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class
{
    
    public IEnumerable<TResult> GetFilteredEntities<TEntity, TResult>(
        Expression<Func<TEntity, TResult>>? mapper = null,
        ISpecification<TEntity>? specification = null) where TEntity : class
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (specification != null)
        {
            query = query.Where(specification.ToExpression());
        }

        if (mapper != null)
        {
            return query.Select(mapper).ToList();
        }

        return query.Cast<TResult>().ToList();
    }
    
    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }
    public async Task<T?> GetByGuidAsync(Guid id)
    {
        var dbSet = context.Set<T>();
        return await dbSet.FindAsync(id);
    }
      
      public async Task Delete(Guid id)
      {
          var entity = await GetByGuidAsync(id);
          if (entity == null) return;
          context.Set<T>().Remove(entity);
      }

      public void Delete(T entity)
      {
          context.Set<T>().Remove(entity);
      }

      public void Update(T entity)
      {
          context.Set<T>().Update(entity);
      }

      public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
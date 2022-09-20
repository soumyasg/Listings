using System.Linq.Expressions;

namespace Listings.API.Services
{
    public interface IGenericCrudService<TEntity, TKey> where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filterExpression = null,
            Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>? sortExpression = null,
            List<string>? includes = null
            );
        
        Task DeleteAsync(TKey id);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<bool> RecordExists(TKey id);
    }
}

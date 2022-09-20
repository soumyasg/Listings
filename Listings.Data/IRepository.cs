using System.Linq.Expressions;

namespace Listings.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filterExpression = null,
            Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>? sortExpression = null,
            List<string>? includes = null
            );
       
        Task DeleteAsync(TKey id);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
    }
}
using Listings.Data;
using Listings.Models;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Listings.Business
{
    public class GenericCrudService<TEntity, TKey> : IGenericCrudService<TEntity, TKey> where TEntity : class, IModelRecord<TKey>, new()
    {
        protected readonly ILogger<GenericCrudService<TEntity, TKey>> _logger;
        protected readonly IRepository<TEntity, TKey> _repository;

        public GenericCrudService(ILogger<GenericCrudService<TEntity, TKey>> logger,
            IRepository<TEntity, TKey> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var newEntity = await _repository.AddAsync(entity);

            return newEntity;


        }
        public async Task<TEntity> AddAsync(TEntity entity, Func<TKey> keyProvider)
        {
            var newEntity = await _repository.AddAsync(entity, keyProvider);

            return newEntity;
        }


        public virtual async Task DeleteAsync(TKey id)
        {
            await _repository.DeleteAsync(id);

        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filterExpression = null, Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>? sortExpression = null, List<string>? includes = null)
        {
            return await _repository.GetAllAsync(filterExpression, sortExpression, includes);
        }
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<bool> RecordExists(TKey id)
        {
            return (await _repository.GetByIdAsync(id)) != null;
        }

        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            TEntity? updatedEntity = null;

            try
            {
                updatedEntity = await _repository.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }

            return updatedEntity;
        }

    }
}

using Listings.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Listings.Data
{
    public abstract class RepositoryBase<TModel, TKey> : IRepository<TModel, TKey> where TModel : class, IModelRecord<TKey>, new() where TKey : struct
    {
        protected static ConcurrentDictionary<TKey, TModel> Records = new();

        public abstract Task<TModel> AddAsync(TModel entity);
        public Task<TModel> AddAsync(TModel entity, Func<TKey> keyProvider)
        {
            // Ignore the id that came with the request and assign one
            // in real life apps, we'll probably be working with a DTO that won't have the Id property
            var newId = keyProvider();
            entity.Id = newId;

            if (!Records.TryAdd(newId, entity))
                throw new Exception("Failed to add record");

            return Task.FromResult(entity);
        }

        public Task DeleteAsync(TKey id)
        {
            var recordToDelete = Records.FirstOrDefault(x => x.Key.Equals(id)).Value;

            if (recordToDelete == null)
                throw new Exception("Item not found");

            if (!Records.TryRemove(id, out var deletedRecord))
                throw new Exception("Failed to delete record");

            return Task.CompletedTask;
        }

        public Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>>? filterExpression = null, Expression<Func<IQueryable<TModel>, IOrderedQueryable<TModel>>>? sortExpression = null, List<string>? includes = null)
        {
            return Task.FromResult(Records.Select(x => x.Value).ToList());
        }

        public Task<TModel?> GetByIdAsync(TKey id)
        {
            var record = Records.FirstOrDefault(x => x.Key.Equals(id)).Value;
            return Task.FromResult(record ?? default);
        }

        public Task<TModel> Update(TModel entity)
        {
            var record = Records.FirstOrDefault(x => x.Key.Equals(entity.Id)).Value;

            if (record == null)
                throw new Exception("Item not found");

            Records[entity.Id] = entity;

            return Task.FromResult(entity);
        }
    }
}

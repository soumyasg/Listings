using System.Linq.Expressions;
using System.Collections.Concurrent;
using Listings.Models;

namespace Listings.Data
{
    public class ListingsRepository : IRepository<ListRecord, int>
    {
        // Making this thread-safe because this is a singleton service
        protected static ConcurrentDictionary<int, ListRecord> ListRecords = new();
        static ListingsRepository()
        {
            Enumerable.Range(1, 10)
                    .ToList()
                    .ForEach(x =>
                    {
                        ListRecords.TryAdd(x, new ListRecord
                        {
                            Id= x,
                            Name = $"Item {x}"
                        });
                    });
        }
        public Task<ListRecord> AddAsync(ListRecord entity)
        {
            // Ignore the id that came with the request and assign one
            // in real life apps, we'll probably be working with a DTO that won't have the Id property
            var newId = ListRecords.Select(x => x.Key).Max() + 1;
            var newRecord = new ListRecord
            {
                Id = newId,
                Name = entity.Name
            };
            ListRecords.TryAdd(newId, newRecord);

            return Task.FromResult(newRecord);
        }
        public Task DeleteAsync(int id)
        {
            var recordToDelete = ListRecords.FirstOrDefault(x => x.Key == id).Value;

            if (recordToDelete == null)
                throw new Exception("Item not found");

            ListRecords.Remove(id, out var deletedRecord);

            return Task.CompletedTask;
        }

        public Task<List<ListRecord>> GetAllAsync(Expression<Func<ListRecord, bool>>? filterExpression = null, Expression<Func<IQueryable<ListRecord>, IOrderedQueryable<ListRecord>>>? sortExpression = null, List<string>? includes = null)
        {
            return Task.FromResult(ListRecords.Select(x => x.Value).ToList());
        }

        public Task<ListRecord?> GetByIdAsync(int id)
        {
            var record = ListRecords.FirstOrDefault(x => x.Key == id).Value;
            return Task.FromResult(record);
        }

        public Task<ListRecord> Update(ListRecord entity)
        {
            var record = ListRecords.FirstOrDefault(x => x.Key == entity.Id).Value;

            if (record == null)
                throw new Exception("Item not found");

            record.Name = entity.Name;

            return Task.FromResult(record);
        }
    }
}

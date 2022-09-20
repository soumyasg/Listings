using Listings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Listings.Data
{
    public class ListingsRepository : IRepository<ListRecord, int>
    {
        static ListingsRepository()
        {
            Enumerable.Range(1, 10)
                    .ToList()
                    .ForEach(x =>
                    {
                        ListRecords.Add(new ListRecord
                        {
                            Id= x,
                            Name = $"Item {x}"
                        });
                    });
        }
        protected static List<ListRecord> ListRecords = new();
        public Task<ListRecord> AddAsync(ListRecord entity)
        {
            // ignore the id that came with the request and assign one
            // in real life apps, we'll probably be working with a DTO that won't have the Id property
            var newId = ListRecords.Select(x => x.Id).Last() + 1;
            var newRecord = new ListRecord
            {
                Id = newId,
                Name = entity.Name
            };
            ListRecords.Add(newRecord);

            return Task.FromResult(newRecord);
        }
        public Task DeleteAsync(int id)
        {
            var recordToDelete = ListRecords.FirstOrDefault(x => x.Id == id);

            if (recordToDelete == null)
                throw new Exception("Item not found");

            ListRecords.Remove(recordToDelete);

            return Task.CompletedTask;
        }

        public Task<List<ListRecord>> GetAllAsync(Expression<Func<ListRecord, bool>>? filterExpression = null, Expression<Func<IQueryable<ListRecord>, IOrderedQueryable<ListRecord>>>? sortExpression = null, List<string>? includes = null)
        {
            return Task.FromResult(ListRecords);
        }

        public Task<ListRecord?> GetByIdAsync(int id)
        {
            var record = ListRecords.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(record);
        }

        public Task<ListRecord> Update(ListRecord entity)
        {
            var record = ListRecords.FirstOrDefault(x => x.Id == entity.Id);

            if (record == null)
                throw new Exception("Item not found");

            record.Name = entity.Name;

            return Task.FromResult(record);
        }
    }
}

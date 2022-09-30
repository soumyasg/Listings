using Listings.Models;

namespace Listings.Data
{
    public class ListingsRepository : RepositoryBase<ListRecord, int>
    {
        static ListingsRepository()
        {
            Enumerable.Range(1, 10)
                    .ToList()
                    .ForEach(x =>
                    {
                        Records.TryAdd(x, new ListRecord
                        {
                            Id= x,
                            Name = $"Item {x}"
                        });
                    });
        }
        public override async Task<ListRecord> AddAsync(ListRecord entity)
        {
            return await this.AddAsync(entity, () => Records.Select(x => x.Key).Max() + 1);
        }
       
    }
}

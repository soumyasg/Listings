using Listings.Data;
using Listings.Models;

namespace Listings.API.Services
{
    public class ListingService : GenericCrudService<ListRecord, int>
    {
        public ListingService(ILogger<GenericCrudService<ListRecord, int>> logger, IRepository<ListRecord, int> _repository) : base(logger, _repository)
        {
        }
    }
}

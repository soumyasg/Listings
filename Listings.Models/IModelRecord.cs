namespace Listings.Models
{
    public interface IModelRecord<TKey>
    {
        public TKey Id { get; set; }
    }
}

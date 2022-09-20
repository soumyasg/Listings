using System.ComponentModel.DataAnnotations;

namespace Listings.Models
{
    public class ListRecord
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }
    }
}
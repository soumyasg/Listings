using System.ComponentModel.DataAnnotations;

namespace Listings.Models
{
    public class ListRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;
    }
}
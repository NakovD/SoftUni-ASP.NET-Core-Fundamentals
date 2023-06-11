using System.ComponentModel.DataAnnotations;
using CategoryConstants = Library.Data.DataConstants.CategoryConstants;

namespace Library.Data.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryConstants.NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
using System.ComponentModel.DataAnnotations;
using BookConstants = Library.Data.DataConstants.BookConstants;

namespace Library.Data.Models
{
    public class Book
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(BookConstants.AuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(BookConstants.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]  
        public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UserBooks { get; set; } = new List<IdentityUserBook>();

    }
}

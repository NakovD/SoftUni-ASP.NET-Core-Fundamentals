using static Library.Data.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookFormModel
    {
        [Required]
        [StringLength(BookConstants.TitleMaxLength, MinimumLength = BookConstants.TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(BookConstants.AuthorMaxLength, MinimumLength = BookConstants.AuthorMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(BookConstants.DescriptionMaxLength, MinimumLength = BookConstants.DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public string Rating { get; set; } = null!;

        [Required]
        [Range(1, double.PositiveInfinity)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel>? Categories { get; set; } = new List<CategoryViewModel>();

    }
}

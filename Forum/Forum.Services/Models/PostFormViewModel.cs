namespace Forum.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostFormViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 30)]
        public string Content { get; set; } = null!;
    }
}

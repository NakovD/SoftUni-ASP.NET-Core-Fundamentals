namespace Forum.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Forum.Data.Constants;

    public class Post
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.Post.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.Post.ContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}

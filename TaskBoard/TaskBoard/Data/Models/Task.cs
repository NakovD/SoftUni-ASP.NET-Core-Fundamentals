using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Data.Models
{
    public class Task
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Task.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.Task.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }

        public Board? Board { get; set; }

        [Required]
        public string OwnerId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}

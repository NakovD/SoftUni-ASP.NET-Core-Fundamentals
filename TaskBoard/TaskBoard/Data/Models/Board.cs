using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Data.Models
{
    public class Board
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Board.BoardNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
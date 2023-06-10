using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;

namespace TaskBoard.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(DataConstants.Task.TaskTitleMaxLength, MinimumLength = DataConstants.Task.TaskTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.Task.TaskDescriptionMaxLength, MinimumLength = DataConstants.Task.TaskDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }
        
        public IEnumerable<TaskBoardModel>? Boards { get; set; }
    }
}

namespace TaskBoard.Models.Task
{
    public class TaskDetailViewModel : TaskViewModel
    {
        public string CreatedOn { get; set; } = null!;

        public int BoardId { get; set; }

        public string OwnerId { get; set; }

        public string Board { get; set; } = null!;
    }
}

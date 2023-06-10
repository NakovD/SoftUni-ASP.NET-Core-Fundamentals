using TaskBoard.Models.Task;

namespace TaskBoard.Models.Board
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}

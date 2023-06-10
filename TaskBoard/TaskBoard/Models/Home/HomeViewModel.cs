namespace TaskBoard.Models.Home
{
    public class HomeViewModel
    {
        public int AllTasksCount { get; set; }

        public List<HomeBoardViewModel> BoardsWithTasksCount { get; set; } = new List<HomeBoardViewModel>();

        public int UserTasksCount { get; set; }
    }
}

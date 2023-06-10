using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoard.Models.Home;
using TaskBoard.Services.Contracts;

namespace TaskBoard.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBoardService boardService;

        private readonly ITaskService taskService;

        public HomeController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var allBoards = await boardService.GetAllAsync();

            var boardsVm = allBoards.Select(b => new HomeBoardViewModel
            {
                BoardName = b.Name,
                TasksCount = b.Tasks.Count,
            }).ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userTasks = await taskService.GetUserTasksCountAsync(userId);

            var vm = new HomeViewModel
            {
                AllTasksCount = boardsVm.Sum(b => b.TasksCount),
                BoardsWithTasksCount = boardsVm,
                UserTasksCount = userTasks
            };

            return View(vm);
        }
    }
}
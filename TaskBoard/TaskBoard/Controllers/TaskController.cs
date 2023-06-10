using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoard.Models.Task;
using TaskBoard.Services.Contracts;

namespace TaskBoard.Controllers
{
    public class TaskController : BaseController
    {
        private readonly IBoardService boardService;

        private readonly ITaskService taskService;

        public TaskController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var boardsVM = await GetBoards();

            var taskFormModel = new TaskFormModel
            {
                Boards = boardsVM
            };

            return View(taskFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            var existingBoards = await GetBoards();
            var doesBoardExist = existingBoards.Any(b => b.Id == taskModel.BoardId);

            if (!doesBoardExist)
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board doesn't exist");
            }

            taskModel.Boards = existingBoards;

            if (!ModelState.IsValid)
            {
                return View(taskModel);
            }

            var currentUserId = GetUserId();

            await taskService.AddSingleAsync(taskModel, currentUserId);

            return RedirectToAction("All", "Board");
        }

        public async Task<IActionResult> Details(int id)
        {
            var neededElement = await taskService.GetSingleByIdAsync(id);

            if (neededElement == null) return NotFound();

            return View(neededElement);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var neededItem = await taskService.GetSingleByIdAsync(id);

            if (neededItem == null) return NotFound();

            var boards = await GetBoards();

            var formVM = new TaskFormModel
            {
                Title = neededItem.Title,
                Description = neededItem.Description,
                BoardId = neededItem.BoardId,
                Boards = boards,
            };
            return View(formVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel taskFormModel)
        {
            var task = await taskService.GetSingleByIdAsync(id);

            if (task == null) return NotFound();

            var userId = GetUserId();

            if (userId != task.OwnerId) return Unauthorized();

            var boards = await GetBoards();
            var doesBoardExist = boards.Any(b => b.Id == taskFormModel.BoardId);

            if (!doesBoardExist)
            {
                ModelState.AddModelError(nameof(taskFormModel.BoardId), "Board with this Id doesnt exist!");
            }

            if (!ModelState.IsValid)
            {
                taskFormModel.Boards = boards;
                return View(taskFormModel);
            }

            var result = await taskService.UpdateSingleAsync(id, taskFormModel);

            if (!result) return BadRequest();

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await taskService.DeleteSingleAsync(id);

            if (!isDeleted) return NotFound();

            return RedirectToAction("All", "Board");
        }

        private async Task<IEnumerable<TaskBoardModel>> GetBoards()
        {
            var boards = await boardService.GetAllAsync();

            return boards.Select(b => new TaskBoardModel
            {
                Id = b.Id,
                Name = b.Name,
            });
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Models.Task;
using TaskBoard.Services.Contracts;
using Task = TaskBoard.Data.Models.Task;

namespace TaskBoard.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly TaskBoardAppDbContext dbContext;

        public TaskService(TaskBoardAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddSingleAsync(TaskFormModel taskFormModel, string userId)
        {
            var task = new Task
            {
                Title = taskFormModel.Title,
                Description = taskFormModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskFormModel.BoardId,
                OwnerId = userId,
            };

            try
            {
                await dbContext.Tasks.AddAsync(task);
                await dbContext.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteSingleAsync(int taskId)
        {
            var neededTask = await dbContext.Tasks.FindAsync(taskId);

            if (neededTask == null) return false;

            dbContext.Tasks.Remove(neededTask);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        public async Task<TaskDetailViewModel?> GetSingleByIdAsync(int id)
        {
            var neededElement = await dbContext.Tasks
                .AsNoTracking()
                .Include(t => t.Board)
                .Include(t => t.Owner)
                .SingleOrDefaultAsync(t => t.Id == id);

            if (neededElement == null) return null;

            var vm = new TaskDetailViewModel
            {
                Title = neededElement.Title,
                Description = neededElement.Description,
                Id = neededElement.Id,
                Owner = neededElement.Owner.UserName,
                CreatedOn = neededElement.CreatedOn.ToString(),
                Board = neededElement.Board?.Name ?? "No board",
                BoardId = neededElement.BoardId,
                OwnerId = neededElement.OwnerId
            };

            return vm;
        }

        public async Task<int> GetUserTasksCountAsync(string userId)
        {
            var neededTasks = await dbContext.Tasks
                .AsNoTracking()
                .Where(t => t.OwnerId == userId)
                .CountAsync();

            return neededTasks;
        }

        public async Task<bool> UpdateSingleAsync(int taskId, TaskFormModel taskFormModel)
        {
            var neededTask = await dbContext.Tasks.FindAsync(taskId);

            if (neededTask == null) return false;

            neededTask.Title = taskFormModel.Title;
            neededTask.Description = taskFormModel.Description;
            neededTask.BoardId = taskFormModel.BoardId;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }
    }
}

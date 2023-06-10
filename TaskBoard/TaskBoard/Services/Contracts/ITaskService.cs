using TaskBoard.Models.Task;


namespace TaskBoard.Services.Contracts
{
    public interface ITaskService
    {
        Task<bool> AddSingleAsync(TaskFormModel taskFormModel, string userId);

        Task<TaskDetailViewModel?> GetSingleByIdAsync(int id);

        Task<bool> UpdateSingleAsync(int taskId, TaskFormModel taskFormModel);

        Task<bool> DeleteSingleAsync(int taskId);

        Task<int> GetUserTasksCountAsync(string userId);
    }
}

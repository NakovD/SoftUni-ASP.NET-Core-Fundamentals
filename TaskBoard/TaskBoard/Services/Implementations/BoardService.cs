using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Models.Board;
using TaskBoard.Models.Task;
using TaskBoard.Services.Contracts;

namespace TaskBoard.Services.Implementations
{
    public class BoardService : IBoardService
    {
        private readonly TaskBoardAppDbContext dbContext;

        public BoardService(TaskBoardAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BoardViewModel>> GetAllAsync()
        {
            return await dbContext
            .Boards
            .AsNoTracking()
            .Include(b => b.Tasks)
            .ThenInclude(b => b.Owner)
            .Select(b => new BoardViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Tasks = b.Tasks.Select(t => new TaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Owner = t.Owner.UserName
                }).ToList()
            }).ToListAsync();
        }
    }
}

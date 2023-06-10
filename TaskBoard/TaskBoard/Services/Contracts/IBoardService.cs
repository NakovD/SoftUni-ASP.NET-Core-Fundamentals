using TaskBoard.Models.Board;

namespace TaskBoard.Services.Contracts
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetAllAsync();
    }
}

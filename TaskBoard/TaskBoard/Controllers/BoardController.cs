using Microsoft.AspNetCore.Mvc;
using TaskBoard.Services.Contracts;

namespace TaskBoard.Controllers
{
    public class BoardController : BaseController
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public IActionResult Index() { return RedirectToAction(nameof(All)); }

        public async Task<IActionResult> All()
        {
            var boards = await boardService.GetAllAsync();

            return View(boards);
        }
    }
}

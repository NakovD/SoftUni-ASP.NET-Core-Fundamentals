using Library.Models;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        private readonly ICategoryService categoryService;

        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
        }

        public IActionResult Index() => RedirectToAction(nameof(All));

        public async Task<IActionResult> All()
        {
            var allBooks = await bookService.GetAllAsync();

            return View(allBooks);
        }

        public async Task<IActionResult> Mine()
        {
            var userId = GetUserId();

            var userBooks = await bookService.GetUserBooksAsync(userId);

            var vm = new BookMineViewModel
            {
                Books = userBooks,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var userId = GetUserId();

            var isActionSuccessfull = await bookService.AddBookToUserCollectionAsync(id, userId);

            if (!isActionSuccessfull) return RedirectToAction(nameof(All));

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var userId = GetUserId();

            var isActionSuccessfull = await bookService.RemoveBookFromUserCollectionAsync(id, userId);

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetAllAsync();

            var vm = new BookFormModel()
            {
                Categories = categories
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel formModel)
        {
            var validCategories = await categoryService.GetAllAsync();

            var isCategoryValid = validCategories.Any(c => c.Id == formModel.CategoryId);

            if (!isCategoryValid)
            {
                ModelState.AddModelError(nameof(formModel.CategoryId), "Invalid category Id!");
            }

            var isDecimalValid = decimal.TryParse(formModel.Rating, out var rating);

            if (!isDecimalValid || rating <= 0 || rating > 10)
            {
                ModelState.AddModelError(nameof(formModel.Rating), "Invalid rating!");

            }

            if (!ModelState.IsValid)
            {
                formModel.Categories = validCategories;
                return View(formModel);
            }

            var isBookAdded = await bookService.AddSingleAsync(formModel);

            if (!isBookAdded) return BadRequest();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}

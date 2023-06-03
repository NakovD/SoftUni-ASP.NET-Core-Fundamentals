namespace Forum.Web.Controllers
{
    using Forum.Services.Contracts;
    using Forum.Services.Models;
    using Microsoft.AspNetCore.Mvc;

    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await postService.GetAllAsync();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(PostFormViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            await postService.AddSingleAsync(model);

            return Redirect(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var neededPost = await postService.GetByIdAsync(id);

            if (neededPost == null) return BadRequest();

            var formVm = new PostFormViewModel
            {
                Title = neededPost.Title,
                Content = neededPost.Content,
            };

             return View(formVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostFormViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var updatedPost = await postService.UpdateSingleAsync(id, model);

            if (updatedPost == null) return BadRequest();

            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await postService.DeleteSingleAsync(id);

            if (!isDeleted) return BadRequest();

            return RedirectToAction(nameof(GetAll));
        }


    }
}

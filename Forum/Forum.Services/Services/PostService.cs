namespace Forum.Services.Services
{
    using System.Collections.Generic;

    using Contracts;
    using Forum.Data;
    using Microsoft.EntityFrameworkCore;
    using Forum.Services.Models;
    using Forum.Data.Models;

    public class PostService : IPostService
    {
        private readonly ForumDbContext context;

        public PostService(ForumDbContext _context)
        {
            context = _context;
        }

        public async Task<PostViewModel> AddSingleAsync(PostFormViewModel model)
        {
            var newPost = new Post()
            {
                Title = model.Title,
                Content = model.Content
            };

            var result = await context.Posts.AddAsync(newPost);

            await context.SaveChangesAsync();

            var vm = new PostViewModel { Id = result.Entity.Id, Title = result.Entity.Title, Content = result.Entity.Content };

            return vm;
        }

        public async Task<bool> DeleteSingleAsync(int id)
        {
            var neededPost = await context.Posts.SingleOrDefaultAsync(p => p.Id == id);

            if (neededPost == null) return false;

            context.Posts.Remove(neededPost);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PostViewModel>> GetAllAsync() =>
                await context.Posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .AsNoTracking()
                .ToListAsync();

        public async Task<PostViewModel?> GetByIdAsync(int id)
        {
            var neededPost = await context.Posts.FindAsync(id);

            if (neededPost == null) return null;

            var vm = new PostViewModel
            {
                Id = neededPost.Id,
                Title = neededPost.Title,
                Content = neededPost.Content
            };

            return vm;
        }

        public async Task<PostViewModel?> UpdateSingleAsync(int id, PostFormViewModel model)
        {
            var neededPost = await context.Posts.FindAsync(id);

            if (neededPost == null) return null;

            neededPost.Title = model.Title;
            neededPost.Content = model.Content;

            await context.SaveChangesAsync();

            var vm = new PostViewModel()
            {
                Id = neededPost.Id,
                Title = neededPost.Title,
                Content = neededPost.Content
            };

            return vm;
        }
    }
}

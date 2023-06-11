using Library.Data;
using Library.Models;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDbContext context;

        public CategoryService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync() => await context.Categories
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
    }
}

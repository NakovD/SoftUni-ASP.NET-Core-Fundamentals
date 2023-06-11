using Library.Data;
using Library.Data.Models;
using Library.Models;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddBookToUserCollectionAsync(int bookId, string userId)
        {
            var neededBook = await context.Books
                .Include(b => b.UserBooks)
                .SingleOrDefaultAsync(b => b.Id == bookId);

            if (neededBook == null) return false;

            var doesUserHasTheBook = neededBook.UserBooks.Any(ub => ub.CollectorId == userId);

            if (doesUserHasTheBook) return false;

            var userBook = new IdentityUserBook
            {
                CollectorId = userId,
                Book = neededBook
            };

            neededBook.UserBooks.Add(userBook);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddSingleAsync(BookFormModel model)
        {
            var newBook = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Author = model.Author,
                ImageUrl = model.Url,
                CategoryId = model.CategoryId,
                Rating = decimal.Parse(model.Rating)
            };

            try
            {
                await context.Books.AddAsync(newBook);
                await context.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync() => await context.Books
            .AsNoTracking()
            .Include(b => b.Category)
            .Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                Rating = b.Rating.ToString(),
                Category = b.Category.Name
            }).ToListAsync();

        public async Task<IEnumerable<BookViewModel>> GetUserBooksAsync(string userId) => await context.Books
            .Where(b => b.UserBooks
                .Where(ub => ub.CollectorId == userId)
                .Count() > 0).
            Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                Rating = b.Rating.ToString(),
                Category = b.Category.Name
            }).ToListAsync();

        public async Task<bool> RemoveBookFromUserCollectionAsync(int bookId, string userId)
        {
            var neededBook = await context.Books
                .Include(b => b.UserBooks)
                .SingleOrDefaultAsync(b => b.Id == bookId);

            if (neededBook == null) return false;

            var doesUserHasTheBook = neededBook.UserBooks.Any(ub => ub.CollectorId == userId);

            if (!doesUserHasTheBook) return false;

            var ubToRemove = neededBook.UserBooks.SingleOrDefault(ub => ub.CollectorId == userId);

            neededBook.UserBooks.Remove(ubToRemove!);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }
    }
}

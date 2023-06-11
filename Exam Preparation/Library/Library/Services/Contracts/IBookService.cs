using Library.Models;

namespace Library.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();

        Task<bool> AddSingleAsync(BookFormModel model);

        Task<IEnumerable<BookViewModel>> GetUserBooksAsync(string userId);

        Task<bool> AddBookToUserCollectionAsync(int bookId, string userId);

        Task<bool> RemoveBookFromUserCollectionAsync(int bookId, string userId);
    }
}

namespace Library.Models
{
    public class BookMineViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; } = new List<BookViewModel>();
    }
}

using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public List<Book> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BookViewModel()
                {
                   Title = book.Title,
                   Genre=((GenreEnum)book.GenreId).ToString(),
                   PublisDate=book.PublishDate.Date.ToString("dd/MM/yyy"),
                   PageConunt=book.PageCount
                });
            }
            return bookList;
        }

    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageConunt { get; set; }
        public string PublisDate { get; set; }
        public string Genre { get; set; }
    }
}


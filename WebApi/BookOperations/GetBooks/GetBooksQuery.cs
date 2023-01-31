using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
            //foreach (var book in bookList)
            //{
            //    vm.Add(new BookViewModel()
            //    {
            //       Title = book.Title,
            //       Genre=((GenreEnum)book.GenreId).ToString(),
            //       PublisDate=book.PublishDate.Date.ToString("dd/MM/yyy"),
            //       PageConunt=book.PageCount
            //    });
            //}
            return vm;
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
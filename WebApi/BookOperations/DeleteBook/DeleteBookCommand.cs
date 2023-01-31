using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Set<Book>().SingleOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Silinecek veri bulunamadı");
            }

            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
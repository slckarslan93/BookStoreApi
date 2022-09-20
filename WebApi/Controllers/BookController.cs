using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations.CreateBooks;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Set<Book>().Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        //FromQuery ile id den bu şekilde de çekebilriz ama BestPractice olan route ile getirmektir

        //[HttpGet]  
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedbook)
        {
            var book = _context.Set<Book>().SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest();
            }
            book.GenreId = updatedbook.GenreId != default ? updatedbook.GenreId : book.GenreId;
            book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;


            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Set<Book>().SingleOrDefault(x => x.Id == id);

            if (book == null)
            {
                return BadRequest();
            }
            //BookList.Remove(book);
            _context.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}

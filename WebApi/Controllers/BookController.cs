using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

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
        public  List<Book> GetList()
        {
            var result = _context.Set<Book>().OrderBy(x=>x.Id).ToList();
            return result;

            //var bookList = _context.OrderBy(x => x.Id).ToList<Book>();
            //return bookList;

            
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Set<Book>().Where(x => x.Id == id).SingleOrDefault();
            return book;


            //var book = BookList.Where(x => x.Id == id).SingleOrDefault();
            //return book;
        }

        //FromQuery ile id den bu şekilde de çekebilriz ama BestPractice olan route ile getirmektir

        //[HttpGet]  
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Set<Book>().SingleOrDefault(x => x.Title == newBook.Title);
            if (book!= null)
            {
                return BadRequest();
            }
            //BookList.Add(newBook);
            _context.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedbook)
        {
            //var book = BookList.SingleOrDefault(x=>x.Id == id);
            var book = _context.Set<Book>().SingleOrDefault(x=>x.Id==id);
            if (book==null)
            {
                return BadRequest();
            }
            book.GenreId = updatedbook.GenreId != default ? updatedbook.GenreId : book.GenreId;
            book.PageCount = updatedbook.PageCount != default ? updatedbook.PageCount : book.PageCount;
            book.PublishDate = updatedbook.PublishDate != default ? updatedbook.PublishDate : book.PublishDate;
            book.Title = updatedbook.Title != default ? updatedbook.Title : book.Title;

            _context.Update(book);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            //var book = BookList.SingleOrDefault(x=>x.Id == id);

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

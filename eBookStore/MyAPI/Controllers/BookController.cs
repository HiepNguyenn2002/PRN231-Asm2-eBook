using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly eBookStoreContext db;
        public BookController(eBookStoreContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult getAllBook()
        {

            var data = db.Books.Include(b => b.Pub).Select(x => new 
            {
                BookId = x.BookId,
                BookName = x.BookName,
                Type = x.Type,
                Price = x.Price,
                Advance = x.Advance,
                Royalty = x.Royalty,
                YtdSales = x.YtdSales,
                Notes = x.Notes,
                PublisherDate = x.PublisherDate,
                Pub = x.Pub

            }).ToList();

            return Ok(data);
        }


        [HttpGet("{id}")]
        public IActionResult getBookById(int id)
        {

            var data = db.Books.Include(b => b.Pub).Where(x => x.BookId == id).Select(x => new
            {
                BookId = x.BookId,
                BookName = x.BookName,
                Type = x.Type,
                Price = x.Price,
                Advance = x.Advance,
                Royalty = x.Royalty,
                YtdSales = x.YtdSales,
                Notes = x.Notes,
                PublisherDate = x.PublisherDate,
                Pub = x.Pub

            }).ToList();

            if (data.Count > 0)
            {
                return Ok(data);
            }
            return NotFound();

          
        }
       [HttpPost]
       public IActionResult AddNewBook(Book book)
        {
           if(book == null)
           {
                return BadRequest();
           }
           db.Books.Add(book);
           db.SaveChanges();
           return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(Book book , int id)
        {
            var b = db.Books.Find(id);
            if (b == null)
            {
                return BadRequest();
            }
            else
            {
                b.BookName = book.BookName;
                b.Type = book.Type;
                b.Price = book.Price;
                b.Advance = book.Advance;
                b.Royalty = book.Royalty;
                b.PublisherDate = book.PublisherDate;
                b.PubId = book.PubId;
                b.Notes = book.Notes;
                b.YtdSales = book.YtdSales;

                db.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = db.Books.FirstOrDefault(x => x.BookId == id);
            if (book == null)
            {
                return NotFound("không tìm thấy book");
            }
            var bookAuthor = db.BookAuthors.Where(x => x.AuthorId == id).ToList();
            if (bookAuthor.Count  == 0)
            {

                db.Remove(book);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return Conflict("Không thể xoá bản ghi này");
            }

        }

    }
}

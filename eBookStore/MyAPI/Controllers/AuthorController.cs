using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly eBookStoreContext db;
        public AuthorController(eBookStoreContext db)
        {
            this.db = db;
        }
        [HttpGet]
       public IActionResult getAllAuthor()
        {

            var data = db.Authors.Include(b => b.BookAuthors).Select(x => new Author
            {
                AuthorId = x.AuthorId,
                BookAuthors = x.BookAuthors,
                Address = x.Address,
                City = x.City,
                FullName = x.FullName,
                EmailAddress = x.EmailAddress,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip

            }).ToList();

            return Ok(data);
        }


        [HttpGet("{id}")]
        public IActionResult getAuthorById(int id)
        {

            var data = db.Authors.Include(b => b.BookAuthors).Where(x => x.AuthorId == id).Select(x => new Author
            {
                AuthorId = x.AuthorId,
                BookAuthors = x.BookAuthors,
                Address = x.Address,
                City = x.City,
                FullName = x.FullName,
                EmailAddress = x.EmailAddress,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip

            }).ToList();

            if (data.Count > 0)
            {
                return Ok(data);
            }
            return NotFound();


        }
        [HttpPost]
        public IActionResult AddNewAuthor(Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            db.Authors .Add(author);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(Author author, int id)
        {
            var b = db.Authors.Find(id);
            if (b == null)
            {
                return BadRequest();
            }
            else
            {
                b.FullName = author.FullName;
                b.Address = author.Address;
                b.City = author.City;
                b.EmailAddress = author.EmailAddress;
                b.Phone = author.Phone;
                b.State = author.State;
                b.Zip = author.Zip;

                db.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var author = db.Authors.FirstOrDefault(x => x.AuthorId == id);
            if (author == null)
            {
                return NotFound("không tìm thấy author");
            }
            var bookAuthor = db.BookAuthors.Where(x => x.AuthorId == id).ToList();
            if (bookAuthor.Count == 0)
            {

                db.Remove(author);
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

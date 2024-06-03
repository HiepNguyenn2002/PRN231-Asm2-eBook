using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly eBookStoreContext db;
        public PublisherController(eBookStoreContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult getAllPublisher()
        {

            var data = db.Publishers.Include(b => b.Books)
                .Include(x => x.Users).Select(x => new Publisher
                {
                PubId = x.PubId,
                City = x.City,
                Users = x.Users,
                Books = x.Books,
                Country = x.Country,
                PublisherName = x.PublisherName,
                State = x.State

            }).ToList();

            return Ok(data);
        }


        [HttpGet("{id}")]
        public IActionResult getPublisherById(int id)
        {

            var data = db.Publishers.Include(b => b.Books)
                .Include(x => x.Users)
                .Where(x => x.PubId == id).Select(x => new Publisher
            {
                PubId = x.PubId,
                City = x.City,
                Users = x.Users,
                Books = x.Books,
                Country = x.Country,
                PublisherName = x.PublisherName,
                State = x.State

            }).ToList();

            if (data.Count > 0)
            {
                return Ok(data);
            }
            return NotFound();


        }
        [HttpPost]
        public IActionResult AddNewPublishers(Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest();
            }
            db.Publishers.Add(publisher);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePublishers(Publisher publisher, int id)
        {
            var b = db.Publishers.Find(id);
            if (b == null)
            {
                return BadRequest();
            }
            else
            {
                b.PublisherName = publisher.PublisherName;
                b.State = publisher.State;
                b.Country = publisher.Country;
                b.City = publisher.City;
                db.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublishers(int id)
        {
            var Publishers = db.Publishers.FirstOrDefault(x => x.PubId == id);
            if (Publishers == null)
            {
                return NotFound("không tìm thấy book");
            }
            var user = db.Users.Where(x => x.PubId == id).ToList();
            var book = db.Books.Where(x => x.PubId == id).ToList();
            if (user.Count == 0 && book.Count == 0)
            {

                db.Remove(Publishers);
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

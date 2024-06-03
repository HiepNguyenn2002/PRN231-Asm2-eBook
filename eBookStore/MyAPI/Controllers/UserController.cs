using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly eBookStoreContext db;
        public UserController(eBookStoreContext db)
        {
            this.db = db;
        }
       
        [HttpGet("{id}")]
        public IActionResult getUserById(int id)
        {

            var data = db.Users.FirstOrDefault(x => x.UserId == id);

            if (data != null)
            {
                return Ok(data);
            }
            return NotFound();


        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            var u = db.Users.Find(user.UserId);
            if (u == null)
            {
                return BadRequest();
            }
            else
            {
                u.EmailAddress = u.EmailAddress;
                u.Password = u.Password; 
               u.FullName = user.FullName;
                u.Source = user.Source;
                u.HireDate = user.HireDate;
                u.PubId = user.PubId;

                db.SaveChanges();
                return Ok();
            }
        }
    }
}

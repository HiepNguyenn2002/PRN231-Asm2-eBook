using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly eBookStoreContext db;
        public RegisterController(eBookStoreContext db)
        {
            this.db = db;
        }


        [HttpPost]
        public IActionResult RegisterUser(User user)
        {

            var u = db.Users.FirstOrDefault(x => x.EmailAddress.Equals(user.EmailAddress));
            if (u == null )
            {
                user.RoleId = 4;
                user.HireDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();


        }
    }
}

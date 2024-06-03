using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly eBookStoreContext db;
        public LoginController(eBookStoreContext db)
        {
            this.db = db;
        }


        [HttpPost]
        public IActionResult LoginUser(User user)
        {

            var u = db.Users.FirstOrDefault(x => x.EmailAddress.Equals(user.EmailAddress));
            if(u != null && user.Password.Equals(u.Password)) {

                return Ok(u);
            }
            return NotFound("Login failed");
           
           
        }



    }
}

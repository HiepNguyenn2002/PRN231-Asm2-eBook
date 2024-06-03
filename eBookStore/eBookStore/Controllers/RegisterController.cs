using eBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
namespace eBookStore.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];
            var cfpassword = Request.Form["cfpassword"];

            if (!password.Equals(cfpassword))
            {
                return RedirectToAction("Index");
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    string apiUrl = "http://localhost:5100/api/Register";
                    var user = new User
                    {
                        EmailAddress = email,
                        Password = password
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, user);

                    // Kiểm tra xem phản hồi có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("Index", "Login");

                    }
                    return RedirectToAction("Index");


                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return RedirectToAction("Index");
                }
            }


        }
    }
}

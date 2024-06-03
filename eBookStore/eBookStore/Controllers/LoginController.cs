using eBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace eBookStore.Controllers
{
    public class LoginController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            using (HttpClient client = new HttpClient())
            {
                try
                {
                   
                    string apiUrl = "http://localhost:5100/api/Login";
                    var user = new User
                    {
                        EmailAddress = email,
                        Password = password
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, user);

                    // Kiểm tra xem phản hồi có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var u = JsonConvert.DeserializeObject<User>(jsonResponse);
                        if(u != null)
                        {
                            HttpContext.Session.SetString("id", u.UserId.ToString());
                            HttpContext.Session.SetString("role", u.RoleId.ToString());
                            HttpContext.Session.SetString("email", u.EmailAddress);

                            if(u.RoleId == 1)
                            {
                                return RedirectToAction("Index", "BookManagement");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                      
                        return RedirectToAction("Index", "Home");
                        
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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using eBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStore.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace eBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var UserID = Convert.ToInt32(HttpContext.Session.GetString("id"));

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "http://localhost:5100/api/User";
                    string apiPublisher = "http://localhost:5100/api/Publisher";
                    List<Publisher> publishers = new List<Publisher>();
                    User u = new User();
                    using (HttpResponseMessage response = await client.GetAsync(apiUrl + "/" + UserID))
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            u = JsonConvert.DeserializeObject<User>(jsonResponse);
                            using (HttpResponseMessage res = await client.GetAsync(apiPublisher))
                            {

                                if (res.IsSuccessStatusCode)
                                {
                                    string jsonres = await res.Content.ReadAsStringAsync();
                                    publishers = JsonConvert.DeserializeObject<List<Publisher>>(jsonres);


                                    ViewData["pubID"] = new SelectList(publishers, "PubId", "PublisherName");


                                }
                            }
                            return View(u);
                            
                        }
                    }
                    
                    return RedirectToAction("Index", "Login");


                }
                catch (Exception ex)
                {

                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return RedirectToAction("Index", "Login");
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile()
        {
            var UserID = Convert.ToInt32(HttpContext.Session.GetString("id"));
            var name = Request.Form["fullName"];
            var source = Request.Form["source"];
            var publisher = Request.Form["publisher"];
            var hireDate = Request.Form["hireDate"];



            using (HttpClient client = new HttpClient())
            {
                try
                {
                    DateTime hire_date;
                    string apiUrl = "http://localhost:5100/api/User";
                    var user = new User();
                    user.UserId = UserID;
                    user.FullName = name;
                    user.Source = source;
                    user.PubId = int.Parse(publisher);
                    user.Password = "test";
                    user.EmailAddress = "Test";
                    //if (DateTime.TryParse(hireDate, out hire_date))
                    //{
                    //    user.HireDate = hire_date;
                    //}

                    HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl , user);

                    // Kiểm tra xem phản hồi có thành công không
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");

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

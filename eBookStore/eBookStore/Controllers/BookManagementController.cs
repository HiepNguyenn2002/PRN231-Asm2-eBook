using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using eBookStore.Models;


namespace eBookStore.Controllers
{
    public class BookManagementController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var UserID = Convert.ToInt32(HttpContext.Session.GetString("id"));

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "http://localhost:5100/api/Book";
                    string apiPublisher = "http://localhost:5100/api/Publisher";
                    List<Book> book = new List<Book>();
                    List<Publisher> publisher = new List<Publisher>();  
                    using (HttpResponseMessage response = await client.GetAsync(apiUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            book = JsonConvert.DeserializeObject<List<Book>>(jsonResponse);
                            using (HttpResponseMessage res = await client.GetAsync(apiPublisher))
                            {

                                if (res.IsSuccessStatusCode)
                                {
                                    string jsonres = await res.Content.ReadAsStringAsync();
                                    publisher = JsonConvert.DeserializeObject<List<Publisher>>(jsonres);

                                    ViewData["pubID"] = new SelectList(publisher, "PubId", "PublisherName");


                                }
                            }
                            ViewData["book"] = book;

                            return View();

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

        public async Task<IActionResult> DeleteBook(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = "http://localhost:5100/api/Book";

                    using (HttpResponseMessage response = await client.DeleteAsync(apiUrl+"/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "BookManagement");

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
        public async Task<IActionResult> AddNewBook()
        {
           
            var name = Request.Form["name"];
            var publisher = Request.Form["Publisher"];
            var Price = Request.Form["Price"];
            var Type = Request.Form["Type"];
            var pubDate = Request.Form["pubDate"];


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    DateTime hire_date;
                    string apiUrl = "http://localhost:5100/api/Book";
                    var book = new Book();
                    book.BookName = name;
                    book.Price = decimal.Parse(Price);
                    book.PubId = int.Parse(publisher);
                    book.Type = Type;
                    book.PublisherDate = DateTime.Parse(pubDate);
                    //if (DateTime.TryParse(hireDate, out hire_date))
                    //{
                    //    user.HireDate = hire_date;
                    //}

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, book);

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
    }
}

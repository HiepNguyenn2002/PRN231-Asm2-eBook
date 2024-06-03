using Microsoft.AspNetCore.Mvc;

namespace eBookStore.Controllers
{
    public class BookManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

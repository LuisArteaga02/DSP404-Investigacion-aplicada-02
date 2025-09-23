using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

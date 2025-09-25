using InvestigacionAplicada02.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InvestigacionAplicada02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Verificar si el usuario está autenticado
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetInt32("UsuarioId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
    }
}
using InvestigacionAplicada02.Models;
using InvestigacionAplicada02.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Linq;
namespace InvestigacionAplicada02.Controllers
{
    public class BibliotecaController : Controller
    {
        private readonly BibliotecaService _servicio;

        public BibliotecaController(BibliotecaService servicio)
        {
            _servicio = servicio;
        }

        public async Task<IActionResult> Index()
        {
            var materiales = await _servicio.ObtenerTodosMateriales();
            return View(materiales);
        }

        public IActionResult CrearLibro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearLibro(Libro libro)
        {
            if (ModelState.IsValid)
            {
                await _servicio.AgregarLibro(libro);
                return RedirectToAction("Index");
            }
            return View(libro);
        }

        public IActionResult CrearRevista()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRevista(revista revista)
        {
            if (ModelState.IsValid)
            {
                await _servicio.AgregarRevista(revista);
                return RedirectToAction("Index");
            }
            return View(revista);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Linq;
using DesafioPractico02.Models;
namespace DesafioPractico02.Controllers
{
    public class BibliotecaController : Controller
    {
        private static List<MaterialBiblioteca> materiales = new List<MaterialBiblioteca>
        {
            new Libro("Cien años de soledad","Gabriel Garcia Marquez","L001", 471),
            new revista("National Geografic","varios","R001","agostro 2025"),
            new Libro("Don Quijote", "Miguel de Cervantes", "L002", 863),
            new revista("Science", "AAAS", "R002", "Septiembre 2025"),
            new Libro("El Principito", "Antoine de Saint-Exupéry", "L003", 96)
        };
        public IActionResult Index()
        {
            var lista = materiales.Select(m => m.MonstrarInformacion()).ToList();
            return View(lista);
        }
        public IActionResult Buscar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Buscar(string codigo)
        {
            var material = materiales.FirstOrDefault(m => m.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (material != null)
            {
                ViewBag.Resultado = material.MonstrarInformacion();
            }
            else
            {
                ViewBag.Resultado = $"No se encontro ningun material con el codigo '{codigo}'";
            }
            return View();
        }
    }
}

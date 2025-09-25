using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using InvestigacionAplicada02.Services;
using InvestigacionAplicada02.Models;

namespace InvestigacionAplicada02.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> FixPasswords()
        {
            var resultado = await _usuarioService.ActualizarPasswordsHash();
            return Content($"Passwords actualizados: {resultado}");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Los métodos Login y Logout permanecen igual...
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email y contraseña son requeridos");
                return View();
            }

            try
            {
                var usuario = await _usuarioService.ValidarLoginAsync(email, password);
                if (usuario != null)
                {
                    // ✅ Verificar que la sesión esté disponible
                    if (HttpContext.Session != null)
                    {
                        HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
                        HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
                        HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

                        // Verificar que se guardaron los valores
                        Console.WriteLine($"Sesión iniciada - UsuarioId: {HttpContext.Session.GetInt32("UsuarioId")}");
                    }
                    else
                    {
                        Console.WriteLine("ADVERTENCIA: HttpContext.Session es null");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenciales inválidas");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al iniciar sesión: {ex.Message}");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

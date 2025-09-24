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
        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario, string password, string confirmarPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // ✅ Usar el método corregido
                    await _usuarioService.RegistrarUsuarioAsync(usuario, password);
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(usuario);
        }
        }
}

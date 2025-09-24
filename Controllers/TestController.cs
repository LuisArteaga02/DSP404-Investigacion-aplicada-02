using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvestigacionAplicada02.Data;

namespace InvestigacionAplicada02.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Servidor = _context.Database.GetDbConnection().DataSource;
                ViewBag.BaseDatos = _context.Database.GetDbConnection().Database;

                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                {
                    // Intentar contar registros para verificar tablas
                    try
                    {
                        var usuariosCount = await _context.Usuarios.CountAsync();
                        var librosCount = await _context.Libros.CountAsync();

                        ViewBag.Mensaje = "✅ CONEXIÓN EXITOSA a Azure SQL Database";
                        ViewBag.Detalles = $"Tablas cargadas correctamente - Usuarios: {usuariosCount}, Libros: {librosCount}";
                    }
                    catch (Exception tableEx)
                    {
                        ViewBag.Mensaje = "⚠️ Conexión exitosa pero las tablas necesitan ser creadas";
                        ViewBag.Detalles = "Ejecuta la migración para crear las tablas";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "❌ ERROR de conexión a Azure SQL Database";
                    ViewBag.Detalles = "Verifica firewall, credenciales y que el servidor esté activo";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "💥 ERROR grave de conexión";
                ViewBag.Detalles = ex.Message;
            }

            return View();
        }
    }
}
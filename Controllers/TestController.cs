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

        public async Task<IActionResult> Conexion()
        {
            var viewModel = new TestConexionViewModel();

            try
            {
                // Test de conexión básica
                viewModel.ConexionExitosa = await _context.CanConnectAsync();

                if (viewModel.ConexionExitosa)
                {
                    // Test de consulta simple
                    var countUsuarios = await _context.Usuarios.CountAsync();
                    var countLibros = await _context.libros.CountAsync();
                    var countRevistas = await _context.revistas.CountAsync();

                    viewModel.Mensaje = $"✅ Conexión exitosa a Azure SQL Database";
                    viewModel.Detalles = $"Usuarios: {countUsuarios} | Libros: {countLibros} | Revistas: {countRevistas}";
                }
                else
                {
                    viewModel.Mensaje = "❌ No se pudo conectar a la base de datos";
                }
            }
            catch (Exception ex)
            {
                viewModel.ConexionExitosa = false;
                viewModel.Mensaje = $"❌ Error: {ex.Message}";
                viewModel.Detalles = ex.StackTrace;
            }

            return View(viewModel);
        }
    }

    public class TestConexionViewModel
    {
        public bool ConexionExitosa { get; set; }
        public string Mensaje { get; set; }
        public string Detalles { get; set; }
    }
}

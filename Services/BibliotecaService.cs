using DesafioPractico02.Models;

namespace InvestigacionAplicada02.Services
{
    public interface BibliotecaService
    {
        Task<List<MaterialBiblioteca>> ObtenerTodosMaterialesAsync();
        Task<List<Libro>> ObtenerTodosLibrosAsync();
        Task<List<revista>> ObtenerTodasRevistasAsync();
        Task<MaterialBiblioteca> ObtenerMaterialPorIdAsync(string codigo);
        Task<Libro> ObtenerLibroPorIdAsync(string codigo);
        Task<revista> ObtenerRevistaPorIdAsync(string codigo);
        Task AgregarLibroAsync(Libro libro);
        Task AgregarRevistaAsync(revista revista);
        Task ActualizarMaterialAsync(MaterialBiblioteca material);
        Task EliminarMaterialAsync(string codigo);
        Task<List<MaterialBiblioteca>> BuscarMaterialesAsync(string codigo);

    }
}

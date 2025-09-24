using Microsoft.EntityFrameworkCore;
using InvestigacionAplicada02.Data;
using DesafioPractico02.Models;

namespace InvestigacionAplicada02.Services

{
    public class BibliotecaService2 : BibliotecaService
    {
        private readonly ApplicationDbContext _context;
        public BibliotecaService2(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<MaterialBiblioteca>> ObtenerTodosMaterialesAsync()
        {
            
            var libros = await _context.libros.ToListAsync();
            var revistas = await _context.revistas.ToListAsync();

            var materiales = new List<MaterialBiblioteca>();
            materiales.AddRange(libros);
            materiales.AddRange(revistas);

            return materiales.OrderBy(m => m.Titulo).ToList();
        }
        public async Task<List<Libro>> ObtenerTodosLibrosAsync()
        {
            return await _context.libros
                .OrderBy(l => l.Titulo)
                .ToListAsync();
        }
        public async Task<List<revista>> ObtenerTodasRevistasAsync()
        {
            return await _context.revistas
                .OrderBy(r => r.Titulo)
                .ToListAsync();
        }
        public async Task<MaterialBiblioteca> ObtenerMaterialPorIdAsync(string codigo)
        {
            
            var libro = await _context.libros.FindAsync(codigo);
            if (libro != null) return libro;

            
            var revista = await _context.revistas.FindAsync(codigo);
            return revista;
        }
        public async Task<Libro> ObtenerLibroPorIdAsync(string codigo)
        {
            return await _context.libros.FindAsync(codigo);
        }
        public async Task<revista> ObtenerRevistaPorIdAsync(string codigo)
        {
            return await _context.revistas.FindAsync(codigo);
        }
        public async Task AgregarLibroAsync(Libro libro)
        {
            _context.libros.Add(libro);
            await _context.SaveChangesAsync();
        }
        public async Task AgregarRevistaAsync(revista revista)
        {
            _context.revistas.Add(revista);
            await _context.SaveChangesAsync();
        }
        public async Task ActualizarMaterialAsync(MaterialBiblioteca material)
        {
            _context.Update(material);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarMaterialAsync(string codigo)
        {
            var material = await ObtenerMaterialPorIdAsync(codigo);
            if (material != null)
            {
                if (material is Libro libro)
                    _context.libros.Remove(libro);
                else if (material is revista revista)
                    _context.revistas.Remove(revista);

                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<MaterialBiblioteca>> BuscarMaterialesAsync(string codigo)
        {
            var libros = await _context.libros
                .Where(l => l.Titulo.Contains(codigo) ||
                           l.Autor.Contains(codigo) ||
                           l.Codigo.Contains(codigo))
                .ToListAsync();

            var revistas = await _context.revistas
                .Where(r => r.Titulo.Contains(codigo) ||
                           r.Autor.Contains(codigo) ||
                           r.Codigo.Contains(codigo))
                .ToListAsync();

            var resultados = new List<MaterialBiblioteca>();
            resultados.AddRange(libros);
            resultados.AddRange(revistas);

            return resultados.OrderBy(m => m.Titulo).ToList();
        }

    }
}

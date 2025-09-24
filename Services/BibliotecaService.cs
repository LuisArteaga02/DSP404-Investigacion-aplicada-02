using Microsoft.EntityFrameworkCore;
using InvestigacionAplicada02.Data;
using InvestigacionAplicada02.Models;

namespace InvestigacionAplicada02.Services

{
    public class BibliotecaService
    {
        private readonly ApplicationDbContext _context;

        public BibliotecaService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<MaterialBiblioteca>> ObtenerTodosMateriales()
        {
            return await _context.Set<MaterialBiblioteca>().ToListAsync();
        }


        public async Task<List<Libro>> ObtenerLibros()
        {
            return await _context.Libros.ToListAsync();
        }

       
        public async Task<List<revista>> ObtenerRevistas()
        {
            return await _context.Revistas.ToListAsync();
        }

     
        public async Task AgregarLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
        }

     
        public async Task AgregarRevista(revista revista)
        {
            _context.Revistas.Add(revista);
            await _context.SaveChangesAsync();
        }

        
        public async Task<List<MaterialBiblioteca>> Buscar(string criterio)
        {
            return await _context.Set<MaterialBiblioteca>()
                .Where(m => m.Titulo.Contains(criterio) ||
                           m.Autor.Contains(criterio) ||
                           m.Codigo.Contains(criterio))
                .ToListAsync();
        }
    }
}

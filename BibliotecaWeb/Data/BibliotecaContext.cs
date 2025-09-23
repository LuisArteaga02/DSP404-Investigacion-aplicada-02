namespace BibliotecaWeb.Data;
using Microsoft.EntityFrameworkCore;
using BibliotecaWeb.Models;
using System.Collections.Generic;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

    public DbSet<Libro> Libros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }
}
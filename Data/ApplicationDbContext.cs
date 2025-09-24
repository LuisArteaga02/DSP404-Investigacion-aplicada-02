using InvestigacionAplicada02.Models;
using InvestigacionAplicada02.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestigacionAplicada02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<revista> Revistas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar TPH (Table Per Hierarchy)
            modelBuilder.Entity<MaterialBiblioteca>()
                .HasDiscriminator<string>("TipoMaterial")
                .HasValue<Libro>("Libro")
                .HasValue<revista>("Revista");

            // Mapear a las tablas existentes
            modelBuilder.Entity<MaterialBiblioteca>().ToTable("MaterialBiblioteca");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        }
    }
}
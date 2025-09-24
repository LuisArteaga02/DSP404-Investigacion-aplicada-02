using DesafioPractico02.Models;
using InvestigacionAplicada02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace InvestigacionAplicada02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> libros { get; set; }
        public DbSet<revista> revistas { get; set; }
        public DbSet<MaterialBiblioteca> materialBiblioteca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MaterialBiblioteca>()
                .HasDiscriminator<string>("TipoMaterial")
                .HasValue<Libro>("Libro")
                .HasValue<revista>("Revistas");

            modelBuilder.Entity<MaterialBiblioteca>(entity =>
            {
                entity.HasKey(m => m.IdMaterial);

                entity.Property(m => m.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(m => m.Autor)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(m => m.Codigo)
                    .IsRequired()
                    .HasMaxLength(50);

                
                entity.Property("TipoMaterial")
                    .IsRequired()
                    .HasMaxLength(20);
            });
            modelBuilder.Entity<Libro>(entity =>
            {
                entity.Property(l => l.NumeroPaginas)
                    .IsRequired();

            });

            modelBuilder.Entity<revista>(entity =>
            {
                entity.Property(r => r.Edicion)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                

                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");


                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });


        }
        public async Task<bool> CanConnectAsync()
        {
            try
            {
                return await Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }
        }


        }
}

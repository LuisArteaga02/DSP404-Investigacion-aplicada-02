namespace BibliotecaWeb.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Libro
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Titulo { get; set; }

        [Required]
        [StringLength(100)]
        public required string Autor { get; set; }

        [Required]
        [StringLength(20)]
        public required string ISBN { get; set; }

        [Range(1900, 2100)]
        public int AñoPublicacion { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}

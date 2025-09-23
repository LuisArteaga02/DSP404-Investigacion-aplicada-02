namespace BibliotecaWeb.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}

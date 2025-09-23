namespace BibliotecaWeb.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Prestamo
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaPrestamo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaDevolucion { get; set; }

        [ForeignKey("Libro")]
        public int LibroId { get; set; }

        public required Libro Libro { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public required Usuario Usuario { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestigacionAplicada02.Models
{
    public class Libro : MaterialBiblioteca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de páginas es requerido")]
        [Range(1, 10000, ErrorMessage = "El número de páginas debe ser mayor a 0")]
        [Display(Name = "Número de Páginas")]
        public int NumeroPaginas { get; set; }


        public Libro() { }
        public Libro(string titulo, string autor, string codigo, int numeropaginas) : base(titulo, autor, codigo)
        {
            NumeroPaginas = numeropaginas;
        }
        public override string MonstrarInformacion()
        {
            return $"[LIBRO] titulo: {Titulo}, Autor: {Autor} , Codigo {Codigo}, paginas {NumeroPaginas}";
        }
    }
}

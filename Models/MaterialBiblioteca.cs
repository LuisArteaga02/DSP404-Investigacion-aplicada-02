using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace InvestigacionAplicada02.Models
{
    public abstract class MaterialBiblioteca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El autor es requerido")]
        [StringLength(100)]
        public string Autor { get; set; }

        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(50)]
        public string Codigo { get; set; }

        

        
        public MaterialBiblioteca() { }
        public MaterialBiblioteca(string titulo, string autor, string codigo)
        {
            Titulo = titulo;
            Autor = autor;
            Codigo = codigo;
        }
        public abstract string MonstrarInformacion();
    }
    



    
}

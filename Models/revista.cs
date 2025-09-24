using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InvestigacionAplicada02.Models
    
{
    public class revista:MaterialBiblioteca
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "La edición es requerida")]
        [StringLength(50)]
        public string Edicion { get; set; }

        public revista() { }
        public revista(string titulo, string autor, string codigo, string edicion) : base(titulo, autor, codigo)
        {
            Edicion = edicion;
        }
        public override string MonstrarInformacion()
        {
            return $"[REVISTA] titulo: {Titulo} , Autor {Autor}, codigo {Codigo}, edicion {Edicion} ";
        }
    }
}

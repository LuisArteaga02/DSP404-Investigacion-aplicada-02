using System.ComponentModel.DataAnnotations;
namespace DesafioPractico02.Models
    
{
    public class revista:MaterialBiblioteca
    {

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

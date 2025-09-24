namespace DesafioPractico02.Models
{
    public class revista:MaterialBiblioteca
    {
        public string Edicion { get; set; }

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

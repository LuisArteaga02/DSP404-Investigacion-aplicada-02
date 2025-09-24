namespace DesafioPractico02.Models
{
    public abstract class MaterialBiblioteca
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Codigo { get; set; }
        public MaterialBiblioteca(string titulo, string autor, string codigo)
        {
            Titulo = titulo;
            Autor = autor;
            Codigo = codigo;
        }
        public abstract string MonstrarInformacion();
    }
    



    
}

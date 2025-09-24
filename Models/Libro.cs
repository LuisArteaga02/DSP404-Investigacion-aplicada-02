namespace DesafioPractico02.Models
{
    public class Libro : MaterialBiblioteca
    {
        public int NumeroPaginas { get; set; }
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

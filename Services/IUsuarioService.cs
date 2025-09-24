using DesafioPractico02.Models;
using InvestigacionAplicada02.Models;
namespace InvestigacionAplicada02.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarUsuarioAsync(Usuario usuario, string password);
        Task<Usuario> ValidarLoginAsync(string email, string password);
        Task<Usuario> ObtenerUsuarioPorEmailAsync(string email);
        Task<bool> EmailExisteAsync(string email);
    }
}

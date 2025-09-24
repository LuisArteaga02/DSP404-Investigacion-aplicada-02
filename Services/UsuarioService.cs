using DesafioPractico02.Models;

using InvestigacionAplicada02.Data;
using InvestigacionAplicada02.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace InvestigacionAplicada02.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario, string password)
        {
            // ✅ CORRECCIÓN: Usar el método correcto
            if (await EmailExisteAsync(usuario.Email))
                throw new Exception("El email ya está registrado");

            // Hashear la contraseña
            usuario.Password = HashPassword(password);
            usuario.FechaRegistro = DateTime.Now;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
        public async Task<Usuario> ValidarLoginAsync(string email, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || !VerifyPassword(password, usuario.Password))
                return null;

            return usuario;
        }
        public async Task<Usuario> ObtenerUsuarioPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}

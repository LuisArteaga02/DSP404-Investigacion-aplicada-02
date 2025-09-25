using InvestigacionAplicada02.Models;

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
            try
            {
                Console.WriteLine($"Buscando usuario: {email}");

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == email);

                Console.WriteLine($"Usuario encontrado: {usuario != null}");

                if (usuario != null)
                {
                    Console.WriteLine($"Password ingresado: {password}");
                    Console.WriteLine($"Password en BD (hash): {usuario.Password}");
                    Console.WriteLine($"Longitud hash: {usuario.Password?.Length}");

                    // Verificar si el hash parece ser de BCrypt
                    if (usuario.Password != null && usuario.Password.StartsWith("$2"))
                    {
                        Console.WriteLine("Parece ser un hash BCrypt válido");
                    }

                    bool passwordValido = VerifyPassword(password, usuario.Password);
                    Console.WriteLine($"Contraseña válida: {passwordValido}");

                    if (passwordValido)
                    {
                        return usuario;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ValidarLoginAsync: {ex.Message}");
                throw;
            }
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
        public async Task<bool> ActualizarPasswordsHash()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                foreach (var usuario in usuarios)
                {
                    // Si la contraseña no es un hash BCrypt, actualizarla
                    if (!usuario.Password.StartsWith("$2"))
                    {
                        Console.WriteLine($"Actualizando password para: {usuario.Email}");
                        string passwordPlano = usuario.Password; // Guardar el texto plano
                        usuario.Password = HashPassword(passwordPlano); // Hashear
                        Console.WriteLine($"Nuevo hash: {usuario.Password}");
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando passwords: {ex.Message}");
                return false;
            }
        }
    }
}

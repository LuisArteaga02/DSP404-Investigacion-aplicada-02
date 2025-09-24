using System.ComponentModel.DataAnnotations;
namespace InvestigacionAplicada02.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarPassword { get; set; }



    }
}

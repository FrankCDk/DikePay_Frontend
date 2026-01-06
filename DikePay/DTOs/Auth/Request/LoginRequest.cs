using System.ComponentModel.DataAnnotations;

namespace DikePay.DTOs.Auth.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Debes ingresar un correo válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 20 caracteres")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}

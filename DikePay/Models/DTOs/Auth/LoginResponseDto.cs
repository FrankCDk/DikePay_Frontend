namespace DikePay.Models.DTOs.Auth
{
    public class LoginResponseDto
    {
        // El Success no viene del JSON, lo manejamos con el StatusCode (200 OK)
        public bool Success { get; set; }

        // Estas deben ser IDENTICAS a las de UserResponse del Backend
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

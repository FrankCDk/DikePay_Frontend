namespace DikePay.Application.DTOs.Auth.Response
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty; // Útil para sesiones largas
        public DateTime Expiration { get; set; }
        public UserDto User { get; set; } = new(); // Para mostrar el nombre en el Dashboard de inmediato
    }

    public class UserDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool State { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

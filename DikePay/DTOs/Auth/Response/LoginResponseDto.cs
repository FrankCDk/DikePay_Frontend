namespace DikePay.DTOs.Auth.Response
{
    public class LoginResponseDto
    {
        // El Success no viene del JSON, lo manejamos con el StatusCode (200 OK)
        public bool Success { get; set; }

        // Estas deben ser IDENTICAS a las de UserResponse del Backend
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool State { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

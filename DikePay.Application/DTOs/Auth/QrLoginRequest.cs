namespace DikePay.Application.DTOs.Auth
{
    public class QrLoginRequest
    {
        public string AuthCode { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;  // Opcional: Para saber desde qué móvil se logueó
    }
}

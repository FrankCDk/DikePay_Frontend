using DikePay.Application.Common;

namespace DikePay.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string user, string password, bool rememberMe);
        Task LogoutAsync();
        Task<bool> VerificarSesionExistenteAsync();
        Task<bool> LoginWithQrCodeAsync(string authCode);
        Task<ServiceResponse<string>> GenerateMobileAccessCodeAsync();

        // Suscripción para recibir el error
        //event Action<string>? OnSyncError;
    }
}

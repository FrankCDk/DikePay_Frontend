namespace DikePay.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string user, string password);
        Task LogoutAsync();
        Task<bool> VerificarSesionExistenteAsync();
    }
}

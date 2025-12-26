using System;
using System.Collections.Generic;
using System.Text;

namespace DikePay.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string user, string password);
        Task LogoutAsync();
        Task<bool> VerificarSesionExistenteAsync();
    }
}

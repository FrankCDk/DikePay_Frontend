using System.Net.Http.Json;
using System.Text.Json;
using DikePay.Application.DTOs.Auth.Response;
using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Services;
using DikePay.Shared.State;

namespace DikePay.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppState _appState;
        private readonly HttpClient _httpClient;
        private readonly INetworkService _connectivityService;
        private readonly IAuthStorage _authStorage;

        public AuthService(AppState appState, IHttpClientFactory httpClientFactory, INetworkService connectivityService, IAuthStorage authStorage)
        {
            _appState = appState;
            _httpClient = httpClientFactory.CreateClient("DikePayApi");
            _connectivityService = connectivityService;
            _authStorage = authStorage;
        }

        public async Task<bool> LoginAsync(string user, string password)
        {
            if (_connectivityService.HasInternet)
            {
                return await LoginOnlineAsync(user, password);
            }

            return await LoginOfflineAsync(user, password);
        }

        private async Task<bool> LoginOnlineAsync(string user, string password)
        {
            try
            {
                // 1. Preparamos el objeto que espera tu Backend (LoginRequest)
                var loginRequest = new { Email = user, Password = password };

                // 2. Realizamos la llamada POST a tu API configurada en MauiProgram
                var response = await _httpClient.PostAsJsonAsync("auth", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    // 3. Leemos la respuesta exitosa
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                    if (result != null)
                    {
                        // Agregamos manualmente el Success si tu API no lo envía pero el objeto llegó
                        result.Success = true;

                        // 4. PERSISTENCIA PARA OFFLINE (Lo que hablamos antes)
                        await _authStorage.SaveValueAsync("last_user", user);
                        await _authStorage.SaveValueAsync("last_pass_hash", password);
                        await _authStorage.SaveValueAsync("user_data", JsonSerializer.Serialize(result));

                        SetAppState(result);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Como Senior te digo: loguea esto en consola para depurar
               Console.WriteLine($"Error en LoginOnline: {ex.Message}");
            }
            return false;
        }

        private async Task<bool> LoginOfflineAsync(string user, string password)
        {
            // Intentar recuperar lo que guardamos la última vez que hubo internet
            var lastUser = await _authStorage.GetValueAsync("last_user");
            var lastPass = await _authStorage.GetValueAsync("last_pass_hash");

            if (user == lastUser && password == lastPass)
            {
                var userDataJson = await _authStorage.GetValueAsync("user_data");
                if (userDataJson != null)
                {
                    var userData = JsonSerializer.Deserialize<LoginResponseDto>(userDataJson);
                    SetAppState(userData!);
                    return true;
                }
            }
            return false;
        }

        private void SetAppState(LoginResponseDto data)
        {
            _appState.EstablecerSesion(new SesionUsuario
            {
                // Actualizamos a los nuevos nombres de propiedades
                NombreCompleto = data.Name,
                Token = data.Token,
                Rol = data.Role,
                CodigoUsuario = data.Code,
                Email = data.Email,
            });
        }

        public async Task LogoutAsync()
        {
            try
            {
                // 1. Borramos los datos sensibles del dispositivo
                _authStorage.RemoveValue("last_user");
                _authStorage.RemoveValue("last_pass_hash");
                _authStorage.RemoveValue("user_data");

                // Si usaste SecureStorage.Default.RemoveAll(), ten cuidado 
                // porque borrará TODO, incluyendo preferencias de usuario o temas.
                // Es mejor borrar solo lo relacionado al Auth.

                // 2. Limpiamos el estado en memoria
                _appState.CerrarSesion();
            }
            catch (Exception ex)
            {
                // Loguear error si SecureStorage falla (raro, pero posible en Windows)
                Console.WriteLine($"Error al cerrar sesión: {ex.Message}");
            }
        }

        public async Task<bool> VerificarSesionExistenteAsync()
        {
            var userDataJson = await _authStorage.GetValueAsync("user_data");
            if (userDataJson != null)
            {
                var userData = JsonSerializer.Deserialize<LoginResponseDto>(userDataJson);
                SetAppState(userData!);
                return true;
            }
            return false;
        }
    }
}

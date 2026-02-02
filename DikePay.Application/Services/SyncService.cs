using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Application.Interfaces.Maui;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;
using DikePay.Shared.State;

namespace DikePay.Application.Services
{
    public class SyncService : ISyncService
    {
        private readonly IArticuloApiService _api;      // El que trae del servidor
        private readonly IArticuloRepository _repo;    // El que guarda en SQLite
        private readonly INotificationService _notifService;
        
        private readonly AppState _appState;
        public event Action<string>? OnSyncCompleted;
        public event Action<string>? OnSyncError;
        public event Action? OnSyncStarted;
        public string? UltimoError { get; private set; }

        public bool HasSyncedThisSession { get; private set; }

        public bool EstaSincronizando { get; private set; }
        public double Progreso { get; private set; }

        public SyncService(
            IArticuloApiService api, 
            IArticuloRepository repo, 
            INotificationService notificationService,            
            AppState appState)
        {
            _api = api;
            _repo = repo;
            _appState = appState;
            _notifService = notificationService;
            
        }

        
        public async Task SincronizarTodoAsync()
        {
            // Si ya está corriendo O si ya se sincronizó en esta sesión, salimos.
            if (EstaSincronizando || HasSyncedThisSession) return;

            try
            {

                EstaSincronizando = true;
                UltimoError = null;
                OnSyncStarted?.Invoke(); // Notificamos que se inicio la sincronización
                _appState.NotifyStateChanged();

                var articulosDto = await _api.GetArticulosFromApiAsync();

                //throw new Exception("ERROR DE PRUEBA");

                if (articulosDto != null && articulosDto.Any())
                {
                    var entidades = articulosDto.Select(MapToEntity).ToList();
                    await _repo.SaveAllAsync(entidades);
                    await _appState.InicializarAppAsync();

                    HasSyncedThisSession = true;
                    OnSyncCompleted?.Invoke($"Se actualizaron {entidades.Count} productos correctamente.");
                }

                OnSyncCompleted?.Invoke("");

            }
            catch (HttpRequestException ex)
            {
                UltimoError = "No se pudo conectar con el servidor. Revisa tu internet.";
                _notifService.Agregar("Falla de Conexión", "Revisa tu internet para actualizar el catálogo.", TipoNotificacion.Error);
                OnSyncError?.Invoke(UltimoError);
            }
            catch (Exception ex)
            {
                // Logueamos el error técnico internamente pero avisamos al usuario
                UltimoError = $"Error crítico: {ex.Message}";
                _notifService.Agregar("Error de Sincronización", ex.Message, TipoNotificacion.Error);
                OnSyncError?.Invoke(UltimoError);
            }
            finally
            {
                EstaSincronizando = false;
                // IMPORTANTE: Volvemos a invocar en el hilo principal al terminar
                _appState.NotifyStateChanged();
                
            }
        }

        private Articulo MapToEntity(ArticuloDto dto) => new Articulo
        {
            Id = dto.Id.ToString(),
            Codigo = dto.Codigo,
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            CodigoSku = dto.Sku
        };

    }
}

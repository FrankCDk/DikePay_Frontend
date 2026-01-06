using DikePay.DTOs.Articulos.Response;
using DikePay.Entities;
using DikePay.Repositories.Interfaces;
using DikePay.Services.Interfaces;
using DikePay.State;

namespace DikePay.Services.Implementations
{
    public class SyncService : ISyncService
    {
        private readonly IArticuloApiService _api;      // El que trae del servidor
        private readonly IArticuloRepository _repo;    // El que guarda en SQLite
        private readonly AppState _appState;
        public event Action<string>? OnSyncCompleted;
        public event Action<string>? OnSyncError;

        public bool HasSyncedThisSession { get; private set; }

        public bool EstaSincronizando { get; private set; }
        public double Progreso { get; private set; }

        public SyncService(IArticuloApiService api, IArticuloRepository repo, AppState appState)
        {
            _api = api;
            _repo = repo;
            _appState = appState;
        }

        
        public async Task SincronizarTodoAsync()
        {
            // Si ya está corriendo O si ya se sincronizó en esta sesión, salimos.
            if (EstaSincronizando || HasSyncedThisSession) return;

            try
            {
                EstaSincronizando = true;
                // Invocamos en el hilo principal para que la UI se entere del inicio
                _appState.NotifyStateChanged();

                var articulosDto = await _api.GetArticulosFromApiAsync();

                if (articulosDto != null && articulosDto.Any())
                {
                    var entidades = articulosDto.Select(MapToEntity).ToList();
                    await _repo.SaveAllAsync(entidades);
                    await _appState.InicializarAppAsync();

                    HasSyncedThisSession = true;
                    OnSyncCompleted?.Invoke($"Se actualizaron {entidades.Count} productos correctamente.");
                }


            }
            catch (HttpRequestException)
            {
                OnSyncError?.Invoke("No se pudo conectar con el servidor. Revisa tu internet.");
            }
            catch (Exception ex)
            {
                // Logueamos el error técnico internamente pero avisamos al usuario
                Console.WriteLine($"Error crítico: {ex.Message}");
                OnSyncError?.Invoke("Ocurrió un error inesperado al sincronizar.");
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
            Codigo = dto.Codigo,
            Nombre = dto.Nombre,
            Precio = dto.Precio,
            CodigoSku = dto.Sku
        };

    }
}

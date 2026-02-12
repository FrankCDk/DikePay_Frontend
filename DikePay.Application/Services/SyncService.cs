using DikePay.Application.DTOs.Articulos.Response;
using DikePay.Application.DTOs.Promocion.Response;
using DikePay.Application.Interfaces;
using DikePay.Application.Interfaces.Repositories;
using DikePay.Application.Interfaces.Services;
using DikePay.Domain.Entities;
using DikePay.Shared.State;

namespace DikePay.Application.Services
{

    /// <summary>
    /// Clase de sincronización de información al momento de ingresar a la aplicación
    /// </summary>
    public class SyncService : ISyncService
    {
        private readonly IArticuloApiService _apiArticulos; // Sincroniza articulos
        private readonly IArticuloRepository _repo;
        private readonly INotificationService _notifService;
        private readonly IPromocionApiService _apiPromociones; // Sincroniza las promociones
        private readonly IPromocionesRepository _promocionesRepository;
        private readonly INetworkService _network;

        private readonly AppState _appState;
        public event Action<string>? OnSyncCompleted;
        public event Action<string>? OnSyncError;
        public event Action? OnSyncStarted;
        public string? UltimoError { get; private set; }

        public bool HasSyncedThisSession { get; private set; }

        public bool EstaSincronizando { get; private set; }
        public double Progreso { get; private set; }

        public SyncService(
            IArticuloApiService apiArticulos,
            IArticuloRepository repo,
            INotificationService notificationService,
            IPromocionApiService apiPromociones,
            IPromocionesRepository promocionesRepository,
            INetworkService network,
            AppState appState)
        {
            _apiArticulos = apiArticulos;
            _repo = repo;
            _appState = appState;
            _notifService = notificationService;
            _apiPromociones = apiPromociones;
            _promocionesRepository = promocionesRepository;
            _network = network;
        }


        public async Task SincronizarTodoAsync()
        {
            if (EstaSincronizando || HasSyncedThisSession) return;

            try
            {

                if (!_network.HasInternet)
                {
                    throw new Exception("Usuario no cuenta con conexión para la sincronización de información.");
                }

                EstaSincronizando = true;
                UltimoError = null;
                OnSyncStarted?.Invoke();
                _appState.NotifyStateChanged();

                // 1. Descarga paralela (Opcional para velocidad)
                var articulosDto = await _apiArticulos.GetArticulosFromApiAsync();
                var promocionesDto = await _apiPromociones.GetPromocionFromApiAsync();

                int conteoArticulos = 0;
                int conteoPromos = 0;

                // 2. Procesar Artículos
                if (articulosDto != null && articulosDto.Any())
                {
                    var entidades = articulosDto.Select(MapToEntity).ToList();
                    await _repo.SaveAllAsync(entidades);
                    conteoArticulos = entidades.Count;
                }

                // 3. Procesar Promociones
                if (promocionesDto != null && promocionesDto.Any())
                {
                    var entidadesPromos = promocionesDto.Select(MapToEntityPromocion).ToList();
                    await _promocionesRepository.SaveAllAsync(entidadesPromos);
                    conteoPromos = entidadesPromos.Count;
                }

                // 4. Finalizar
                await _appState.InicializarAppAsync();
                HasSyncedThisSession = true;

                string mensajeExito = $"Sincronización completa: {conteoArticulos} productos y {conteoPromos} promociones.";
                OnSyncCompleted?.Invoke(mensajeExito);
            }
            catch (Exception ex)
            {
                // Logueamos el error técnico internamente pero avisamos al usuario
                UltimoError = $"Error crítico: {ex.Message}";
                await _notifService.Agregar("Error de Sincronización", ex.Message, TipoNotificacion.Error);
                OnSyncError?.Invoke(UltimoError);
            }
            finally
            {
                EstaSincronizando = false;
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

        private Promocion MapToEntityPromocion(PromocionDto dto) => new Promocion
        {
            Id = dto.Id.ToString(),
            CodigoPromocion = dto.CodigoPromocion,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            TipoPromocion = dto.TipoPromocion,
            ArticuloId = dto.ArticuloId.ToString(),
            CantidadMinima = dto.CantidadMinima,
            NuevoPrecio = dto.NuevoPrecio,
            PorcentajeDescuento = dto.PorcentajeDescuento,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            Estado = dto.Estado
        };
    }
}

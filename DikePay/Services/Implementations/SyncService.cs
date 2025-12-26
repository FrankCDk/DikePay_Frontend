using DikePay.Models.Facturacion;
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
            if (EstaSincronizando) return;

            try
            {
                EstaSincronizando = true;
                _appState.NotifyStateChanged(); // Avisamos a la UI para que muestre el spinner

                // 1. Traemos los datos del API
                //var articulosDto = await _api.GetArticulosFromApiAsync();

                //if (articulosDto.Any())
                //{
                //    // 2. Guardamos en SQLite (podemos hacerlo en lotes para ser más rápidos)
                //    await _repo.SaveAllAsync(articulosDto.Select(dto => new Articulo
                //    {
                //        Codigo = dto.Codigo,
                //        Nombre = dto.Nombre,
                //        Precio = dto.Precio,
                //        CodigoSku = dto.CodigoSku,
                //    }).ToList());

                //    // 3. Actualizamos el catálogo en memoria del AppState
                //    await _appState.InicializarAppAsync();
                //}
            }
            finally
            {
                EstaSincronizando = false;
                _appState.NotifyStateChanged();
            }
        }
      
    }
}

using DikePay.Models.Auth;
using DikePay.Models.Facturacion;
using DikePay.Repositories.Interfaces;

namespace DikePay.State
{
    public class AppState
    {
        private readonly IArticuloRepository _articuloRepository;
        public SesionUsuario? UsuarioActivo { get; private set; }

        public AppState(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        public List<Articulo> ArticulosLocales { get; private set; } = new();
        public event Action? OnChange;

        public async Task InicializarAppAsync()
        {
            ArticulosLocales = await _articuloRepository.GetAllAsync();
            NotifyStateChanged();
        }

        // CAMBIO SENIOR: Ahora es PUBLIC para que SyncService pueda usarlo
        public void NotifyStateChanged() => OnChange?.Invoke();

        // Agregué este método para el Login que hablamos antes
        public void EstablecerSesion(SesionUsuario sesion)
        {
            // Aquí guardarías el usuario activo si ya tienes el modelo
            UsuarioActivo = sesion;
            NotifyStateChanged();
        }

        public void CerrarSesion()
        {
            UsuarioActivo = null;
            // También podrías limpiar el carrito aquí si lo deseas
            // ArticulosLocales.Clear(); 
            NotifyStateChanged(); // ¡Importante! Avisar a la radio que el usuario se fue
        }
    }
}

using DikePay.Models.Auth;

namespace DikePay.State
{
    public class AppState
    {
        public SesionUsuario? UsuarioActivo { get; private set; }

        public AppState()
        {
        }

        public event Action? OnChange;

        public async Task InicializarAppAsync()
        {
            NotifyStateChanged();
        }

        // CAMBIO SENIOR: Ahora es PUBLIC para que SyncService pueda usarlo
        //public void NotifyStateChanged() => OnChange?.Invoke();

        public void NotifyStateChanged()
        {
            // Usamos el despachador de MAUI para asegurarnos de que el evento 
            // siempre se dispare en el hilo correcto, sin importar quién lo llame.
            MainThread.BeginInvokeOnMainThread(() => OnChange?.Invoke());
        }

        // Agregué este método para el Login que hablamos antes
        public void EstablecerSesion(SesionUsuario sesion)
        {
            UsuarioActivo = sesion;
            NotifyStateChanged();
        }

        public void CerrarSesion()
        {
            UsuarioActivo = null;
            NotifyStateChanged(); // ¡Importante! Avisar a la radio que el usuario se fue
        }
    }
}

namespace DikePay.Shared.State
{
    public class AppState
    {
        public SesionUsuario? UsuarioActivo { get; private set; }

        public event Action? OnChange;
        public bool IsOnline { get; private set; } = true;

        public Task InicializarAppAsync()
        {
            NotifyStateChanged();
            return Task.CompletedTask;
        }

        public void EstablecerSesion(SesionUsuario sesion)
        {
            UsuarioActivo = sesion;
            NotifyStateChanged();
        }

        public void CerrarSesion()
        {
            UsuarioActivo = null;
            NotifyStateChanged();
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void SetConnectivity(bool isOnline)
        {
            if (IsOnline == isOnline) return;

            IsOnline = isOnline;
            NotifyStateChanged();
        }
    }
}